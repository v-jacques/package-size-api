namespace PackageSize.Domain

open System

type ProductType =
    | PhotoBook = 0
    | Calendar = 1
    | Canvas = 2
    | Cards = 3
    | Mug = 4

[<CLIMutable>]
type OrderItem =
    { OrderItemID: int
      ProductType: ProductType
      Quantity: int }

[<CLIMutable>]
type Order =
    { OrderID: int
      OrderItems: OrderItem seq }
    /// Minimum bin width required for the order in millimeters (mm).
    member this.RequiredBinWidth =
        this.OrderItems
        |> Seq.map (fun item ->
            match item.ProductType with
            | ProductType.PhotoBook -> 19.0 * float item.Quantity
            | ProductType.Calendar -> 10.0 * float item.Quantity
            | ProductType.Canvas -> 16.0 * float item.Quantity
            | ProductType.Cards -> 4.7 * float item.Quantity
            // Mugs can be stacked onto each other (up to 4 in a stack).
            | ProductType.Mug -> 94.0 * Math.Ceiling(float item.Quantity / 4.0)
            | _ -> 0.0)
        |> Seq.sum
