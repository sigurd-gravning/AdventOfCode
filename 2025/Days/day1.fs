module day1

open System.IO

let secretEntrance =
    let path = Path.Combine(__SOURCE_DIRECTORY__, "..", "Data", "input-day1.txt")
    let lines = File.ReadAllLines(path)
    
    
    // Convert lines into a list
    let rotations = lines |> Seq.toList
    
    let parseRotation (position: int) (rotation: string) =
        let direction = rotation.[0]
        let amount = int rotation.[1..]
        match direction with
        | 'L' -> position - amount
        | 'R' -> position + amount
        | _ -> failwith "Unknown direction"
        
    let checkCorrectPos pos =
        pos % 100 = 0
            
    let secretPassword (startPosition: int) rotations =
        let rec loop rotations position count =
            match rotations with
            | [] -> count
            | head :: tail ->
                let rotation = parseRotation position head
                if checkCorrectPos rotation then loop tail rotation (count + 1)
                else loop tail rotation count
        loop rotations startPosition 0
    
    secretPassword 100050 rotations
    
let newPasswordMethod =
    let path = Path.Combine(__SOURCE_DIRECTORY__, "..", "Data", "input-day1.txt")
    let lines = File.ReadAllLines(path)

    let rotations = lines |> Seq.toList

    let parseRotation (position: int) (rotation: string) =
        let direction = rotation.[0]
        let amount = int rotation.[1..]
        match direction with
        | 'L' -> position - amount
        | 'R' -> position + amount
        | _ -> failwith "Unknown direction"
        
    let checkCorrectPos pos =
        pos % 100 = 0

    let countCrossings oldPos newPos =
        abs((newPos / 100) - (oldPos / 100))

    let secretPassword (startPosition: int) rotations =
        let rec loop rotations position count =
            match rotations with
            | [] -> count
            | head :: tail ->
                let newPosition = parseRotation position head
                let crossings = countCrossings position newPosition
                if checkCorrectPos position && head[0] = 'L' && checkCorrectPos newPosition then loop tail newPosition (count + crossings)
                else if checkCorrectPos position && head[0] = 'L' then loop tail newPosition (count + crossings - 1) 
                else if head[0] = 'L' && checkCorrectPos newPosition then loop tail newPosition (count + crossings + 1) 
                else loop tail newPosition (count + crossings)
        loop rotations startPosition 0 

    secretPassword 100050 rotations