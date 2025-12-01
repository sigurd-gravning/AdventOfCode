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
    
    secretPassword 10050 rotations
    
let newPasswordMethod =
    let path = Path.Combine(__SOURCE_DIRECTORY__, "..", "Data", "input-test.txt")
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
        abs(newPos / 100 - oldPos / 100)

    let secretPassword (startPosition: int) rotations =
        let rec loop rotations position count =
            match rotations with
            | [] -> count
            | head :: tail ->
                let newPosition = parseRotation position head
                let crossings = countCrossings position newPosition
                loop tail newPosition (count + crossings)
        loop rotations startPosition 0

    secretPassword 1000 rotations