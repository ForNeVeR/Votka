namespace Votka

type DatasetType =
    | StructuredPoints
    | StructuredGrid
    | RectilinearGrid
    | PolyData
    | UnstructuredGrid
    | Field

type DataType =
    | Bit
    | UnsignedChar
    | Char
    | UnsignedShort
    | Short
    | UnsignedInt
    | Int
    | UnsignedLong
    | Long
    | Float
    | Double

type PointsHeader =
    { number : int
      dataType : DataType }
