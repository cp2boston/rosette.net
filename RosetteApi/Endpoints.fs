namespace Rosette

open FSharp.Data
open Microsoft.FSharp.Reflection
open RosetteParameters

type RosetteResponse = { statusCode: int; headers: System.Collections.Generic.IDictionary<string, string>; body: string }

module Rosette = 

    let GetUnionCaseName (x: 'a) = 
        match FSharpValue.GetUnionFields(x, typeof<'a>) with
        | case, _ -> case.Name

    type Endpoint = 
        | Ping
        | Info
        | Entities
        member this.AsString = 
            match this with
            | Ping -> "ping"
            | Info -> "info"
            | Entities -> "entities"
    
    let (|EndsWithSlash|) (s : string) = s.EndsWith("/")

    let private setUrl url (endpoint: Endpoint) = 
        match url with
        | null -> "https://api.rosette.com/rest/v1/" + endpoint.AsString
        | EndsWithSlash true -> url + endpoint.AsString
        | _ -> url + "/" + endpoint.AsString


    let private rosetteHeaders = [ "Accept", "applicationJson";
                           "Content-Type", "application/json";
                           "X-RosetteAPI-Binding", ".NET";
                           "X-RosetteAPI-Binding-Version", "1.6.0"
                         ]

    let private getHeaders key = 
        Seq.append rosetteHeaders  [ "X-RosetteApi-Key", key ]

    let private headers responseHeaders = 
        responseHeaders |> Map.toSeq |> dict

    let private body responseBody = 
        match responseBody with
        | Text text -> text
        | Binary bytes -> ""

    type Api = {
        endpoint: Endpoint;
        apiKey: string;
        url: string;
        parameters: Parameters
        } with 
        member this.AltUrl url = 
            {this with url = url}
        member this.Execute = 
            let response = Http.Request(url = setUrl this.url this.endpoint, headers = getHeaders this.apiKey, silentHttpErrors = true)
            {statusCode = response.StatusCode; headers = headers(response.Headers); body = body(response.Body)}
        

    let callEndpoint endpoint apiKey parameters = 
        { endpoint = endpoint; apiKey = apiKey; url = "https://api.rosette.com/rest/v1"; parameters = parameters }
        
    let entities apikey url = 
        let response = Http.Request(url = setUrl url Endpoint.Entities, headers = getHeaders apikey, silentHttpErrors = true, httpMethod = "POST")

        {statusCode = response.StatusCode; headers = headers(response.Headers); body = body(response.Body)}

