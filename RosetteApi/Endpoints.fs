namespace Rosette

open FSharp.Data
open Microsoft.FSharp.Reflection
open DocumentParameters

type RosetteResponse = { statusCode: int; headers: System.Collections.Generic.IDictionary<string, string>; body: string }

module Rosette = 

    type Endpoint = 
        | Categories
        | Entities
        | Info
        | Language
        | Ping
        | Relationships
        | Sentences
        | Sentiment
        | SyntaxDependencies
        | Tokens
        | TextEmbedding
        member this.AsString = 
            match this with
            | Categories -> "categories"
            | Entities -> "entities"
            | Info -> "info"
            | Language -> "language"
            | Ping -> "ping"
            | Relationships -> "relationships"
            | Sentences -> "sentences"
            | Sentiment -> "sentiment"
            | SyntaxDependencies -> "syntax/dependencies"
            | Tokens -> "tokens"
            | TextEmbedding -> "text-embedding"
    
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
            let response = 
                match this.endpoint with
                | Ping | Info -> Http.Request(url = setUrl this.url this.endpoint, headers = getHeaders this.apiKey, silentHttpErrors = true)
                | _ -> Http.Request(url = setUrl this.url this.endpoint, headers = getHeaders this.apiKey, body = TextRequest this.parameters.AsJson, silentHttpErrors = true, httpMethod = "POST")

            {statusCode = response.StatusCode; headers = headers(response.Headers); body = body(response.Body)}
        

    let callEndpoint endpoint apiKey parameters = 
        { endpoint = endpoint; apiKey = apiKey; url = "https://api.rosette.com/rest/v1"; parameters = parameters }
        

