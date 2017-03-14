namespace Rosette

open System
open Newtonsoft.Json
open Microsoft.FSharpLu.Json

module DocumentParameters = 


    type Parameters = {
        content: string option;
        contentUri: string option;
        genre: string option;
        language: string option
        } with
        [<JsonIgnore>]
        member this.AsJson = 
            Common.AsJsonString this
        member this.SetLanguage langCode = 
            {this with language = Some langCode}
        member this.SetGenre genreType = 
            {this with genre = Some genreType}

    let (|IsUri|) (s : string) = Uri.IsWellFormedUriString(s, UriKind.Absolute)

    let Create content = 
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
        Create "test"
        |> Language "eng"
        |> AsJson




    

