# ASCII Directory Tree generator

By either running the executable and following the provided directions, or by using it as a CLI and passing in an argument when ran from a terminal, the console application returns an ASCII representation of the provided directory and its content.

## CLI Info

`treegen.exe [file_path]` - The CLI only takes in a single argument, the path to the root directory.

After it's ran, it will create a `.txt` file in the same directory the root directory of the tree is in.  
The `.txt` will contain the ASCII Directory Tree.

## Example Output

```
~/Home/
├── To-Do list.txt
├── Cat pictures/
│   ├── IMG001.png
│   ├── IMG002.png
│   └── IMG003.png
└── Important files/
    ├── config.json
    └── passwords.txt
```
