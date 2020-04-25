﻿module Editor

open JunUtils

type EditorSetting = 
    | Bookmarks       of list<int>
    | DistanceSpacing of decimal
    | BeatDivisor     of decimal
    | GridSize        of int
    | TimelineZoom    of decimal
    | Comment         of string

let tryParseEditorOption line : EditorSetting option =
    match line with
    | Regex @"(.+)\s?:\s?(.+)" [key; value] ->
        match key with
        | "Bookmarks"        ->
            match tryParseCsvInt value with
            | Some(list) -> Some(Bookmarks(list))
            | None -> printfn "Error parsing %s" value; Some(Comment(line))
        | "DistanceSpacing"  -> Some(DistanceSpacing(decimal value))
        | "BeatDivisor"      -> Some(BeatDivisor(decimal value))
        | "GridSize"         -> Some(GridSize(int value))
        | "TimelineZoom"     -> Some(TimelineZoom(decimal value))
        | _ -> Some(Comment(line))
    | _ -> Some(Comment(line))

let parseEditorSection = parseSectionUsing tryParseEditorOption
