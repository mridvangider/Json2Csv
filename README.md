# Json2Csv

A simple JSON to CSV converter. It works with some assumptions:
- Input file must have `.json` extension.
- Output file must hace `.csv` extension.
- All the JSON objects in the input file must be simple, must not contain nested objects or arrays.
- All the JSON objects in the input file must be inside a top level array.
- Header is included in the output CSV file.
- All column names in the header are enclosed with `"`.
- All string and date values are enclosed with `"`.
- `.` is used for decimal seperator in the float attributes.
- Attribute names and values must not contain `"`.