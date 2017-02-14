namespace Rosette

open System
open Newtonsoft.Json
open Microsoft.FSharpLu.Json

module RosetteParameters = 
    
    type Parameters = {
        content: Option<string>;
        contentUri: Option<string>;
        genre: Option<string>;
        language: Option<string>
        } with
        [<JsonIgnore>]
        member this.AsJson = 
            JsonConvert.DefaultSettings <- fun() ->
                let settings = new JsonSerializerSettings()
                settings.NullValueHandling <- NullValueHandling.Ignore
                settings

            Compact.serialize(this)
        member this.SetLanguage langCode = 
            {this with language = Some langCode}
        member this.SetGenre genreType = 
            {this with genre = Some genreType}

    let (|IsUri|) (s : string) = Uri.IsWellFormedUriString(s, UriKind.Absolute)

    let DocumentParameters content = 
        match content with
        | IsUri true -> {content=None; contentUri=Some content; genre=None; language=None}
        | _ -> {content=Some content; contentUri=None; genre=None; language=None}

    let Genre value (parameters:Parameters) = 
        parameters.SetGenre value

    let Language value (parameters:Parameters) = 
        parameters.SetLanguage value

    let AsJson (parameters:Parameters) = 
        parameters.AsJson


    let foo = 
        DocumentParameters "test"
        |> Language "eng"
        |> AsJson




    

