namespace Rosette

open Newtonsoft.Json

module NameSimilarityParameters =
    
    type nameOptions = {
        entity_type: string option;
        language: string option;
        script: string option
    }

    type Parameters = {
        text: string;
        options: nameOptions option
        } with
        [<JsonIgnore>]
        member this.AsJson = 
            Common.AsJsonString this
        member this.SetEntityOption entityType =                 
            {this with options = Some {this.options.Value with entity_type = entityType}}         
        member this.SetLanguageOption langCode =                 
            {this with options = Some {this.options.Value with language = langCode}}       
        member this.SetScriptOption scriptCode =                 
            {this with options = Some {this.options.Value with script = scriptCode}}

    // Name Parameters

    let NameParameters text =
        {text=text; options=None}

    let EntityOption (parameters:Parameters) entityType = 
        parameters.SetEntityOption entityType   
        
    let LanguageOption (parameters:Parameters) langCode = 
        parameters.SetLanguageOption langCode

    let ScriptOption (parameters:Parameters) scriptCode = 
        parameters.SetScriptOption scriptCode