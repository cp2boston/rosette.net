namespace Rosette
open Newtonsoft.Json


module NameTranslationParameters =

    type Parameters = {
        name: string;
        targetLanguage: string;
        entityType: string option;
        genre: string option;
        sourceLanguageOfOrigin: string option;
        sourceLanguageOfUse: string option;
        sourceScript: string option;
        targetScript: string option;
        targetScheme: string option;
        } with
        [<JsonIgnore>]
        member this.AsJson = 
            Common.AsJsonString this
        member this.SetEntityType entityType = 
            {this with entityType = Some entityType}
        member this.SetGenre genre =
            {this with genre = Some genre}
        member this.SetSourcelanguageOfOrigin langCode = 
            {this with sourceLanguageOfOrigin = Some langCode}
        member this.SetSourcelanguageOfUse langCode = 
            {this with sourceLanguageOfUse = Some langCode}
        member this.SetSourceScript sourceScript = 
            {this with sourceScript = Some sourceScript}
        member this.SetTargetScript targetScript = 
            {this with targetScript = Some targetScript}
        member this.SetTargetScheme scheme = 
            {this with targetScheme = Some scheme}

    let NameTranslationParameters name targetLanguage = 
        {name=name; targetLanguage=targetLanguage; entityType=None; genre=None; sourceLanguageOfOrigin=None; sourceLanguageOfUse=None; sourceScript=None; targetScript=None; targetScheme=None}

    let Genre value (parameters:Parameters) = 
        parameters.SetGenre value