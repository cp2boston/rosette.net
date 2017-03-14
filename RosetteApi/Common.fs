namespace Rosette

open System
open Newtonsoft.Json
open Microsoft.FSharpLu.Json

module Common =

    let AsJsonString parameterObject = 
        JsonConvert.DefaultSettings <- fun() ->
            let settings = new JsonSerializerSettings()
            settings.NullValueHandling <- NullValueHandling.Ignore
            settings

        Compact.serialize(parameterObject)