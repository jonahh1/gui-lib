namespace GUI_Lib
{
    public struct Function {
        public MethodInfo function;
        public string[] args;
    }
    public class StaticStyle {
        public Color backgroundCol;
        public Color foregroundCol;
        
        public Color borderCol;
        public float borderWidth;

        public Rectangle rect;
        public Rectangle parent;
        public AnchorType anchor;

        public string text;
        public AnchorType textAlign;
        public float fontSize;
        public float fontSpacing; public void SetFontSpacing(float v) {fontSpacing = v; Console.WriteLine(fontSpacing);}
        public Font font;

        public List<MethodInfo> functions = new List<MethodInfo>();

        public StaticStyle()
        {
            backgroundCol = Color.GRAY;
            foregroundCol = Color.BLACK;
            borderCol = Color.WHITE;

            borderWidth = 3;

            rect = new Rectangle(0,0,100,100);
            parent = new Rectangle(0,0,Raylib.GetScreenWidth(),Raylib.GetScreenHeight());
            anchor = AnchorType.top_left;

            text = "";
            textAlign = AnchorType.middle_center;
            fontSize = 20;
            fontSpacing = 0;
            font = Raylib.GetFontDefault();

        }
    }
    public class DynamicStyle {
        public StaticStyle baseStyle;
        public Dictionary<string, StaticStyle> styles;
        public DynamicStyle(StaticStyle baseStyle, Dictionary<string, StaticStyle> styles) {
            this.baseStyle = baseStyle;
            this.styles = styles;
        }

        public bool IsMouseHovering() {
            StaticStyle s = styles.First().Value;
            return IsMouseHovering(GUITools.ModRectFromParentAndAnchor(s.parent, s.anchor, s.rect));
        }
        public bool IsMouseHovering(Rectangle overide) {
            return Raylib.CheckCollisionPointRec(Raylib.GetMousePosition(), overide);
        }

        public bool IsMousePressed(MouseButton mb = MouseButton.MOUSE_BUTTON_LEFT){
            return IsMouseHovering() && Raylib.IsMouseButtonPressed(mb);
        }
        public bool IsMouseDown(MouseButton mb = MouseButton.MOUSE_BUTTON_LEFT){
            return IsMouseHovering() && Raylib.IsMouseButtonDown(mb);
        }
        public bool IsMouseReleased(MouseButton mb = MouseButton.MOUSE_BUTTON_LEFT){
            return IsMouseHovering() && Raylib.IsMouseButtonReleased(mb);
        }

        public bool IsMousePressed(Rectangle overide, MouseButton mb = MouseButton.MOUSE_BUTTON_LEFT){
            return IsMouseHovering(overide) && Raylib.IsMouseButtonPressed(mb);
        }
        public bool IsMouseDown(Rectangle overide, MouseButton mb = MouseButton.MOUSE_BUTTON_LEFT){
            return IsMouseHovering(overide) && Raylib.IsMouseButtonDown(mb);
        }
        public bool IsMouseReleased(Rectangle overide, MouseButton mb = MouseButton.MOUSE_BUTTON_LEFT){
            return IsMouseHovering(overide) && Raylib.IsMouseButtonReleased(mb);
        }
    }
    public static class ScriptEngine
    {
        
        public static Dictionary<string, string> Variables = new Dictionary<string, string>();
        public static Dictionary<string, StaticStyle> StaticStyles = new Dictionary<string, StaticStyle>();
        public static Dictionary<string, DynamicStyle> DynamicStyles = new Dictionary<string, DynamicStyle>();
        public static Dictionary<string, Font> Fonts = new Dictionary<string, Font>();
        public static Color BGCol = Color.WHITE;
        public static string Namespace = "GUI_Lib";
        static char cmdPrefix = '@';
        static char varPrefix = '$';
        static string spaceMatch = @"(""[^""\\]*(?:\\.[^""\\]*)*"")|\s+";

        public static string Path = "";
        public static List<string> FromatedCode = new List<string>();

        public static void AddCols()
        {
            // the worst code imaginable...
            Variables.Add("AliceBlue",            "#F0F8FF");
            Variables.Add("AntiqueWhite",         "#FAEBD7");
            Variables.Add("Aqua",                 "#00FFFF");
            Variables.Add("Aquamarine",           "#7FFFD4");
            Variables.Add("Azure",                "#F0FFFF");
            Variables.Add("Beige",                "#F5F5DC");
            Variables.Add("Bisque",               "#FFE4C4");
            Variables.Add("Black",                "#000000");
            Variables.Add("BlanchedAlmond",       "#FFEBCD");
            Variables.Add("Blue",                 "#0000FF");
            Variables.Add("BlueViolet",           "#8A2BE2");
            Variables.Add("Brown",                "#A52A2A");
            Variables.Add("BurlyWood",            "#DEB887");
            Variables.Add("CadetBlue",            "#5F9EA0");
            Variables.Add("Chartreuse",           "#7FFF00");
            Variables.Add("Chocolate",            "#D2691E");
            Variables.Add("Coral",                "#FF7F50");
            Variables.Add("CornflowerBlue",       "#6495ED");
            Variables.Add("Cornsilk",             "#FFF8DC");
            Variables.Add("Crimson",              "#DC143C");
            Variables.Add("Cyan",                 "#00FFFF");
            Variables.Add("DarkBlue",             "#00008B");
            Variables.Add("DarkCyan",             "#008B8B");
            Variables.Add("DarkGoldenRod",        "#B8860B");
            Variables.Add("DarkGray",             "#A9A9A9");
            Variables.Add("DarkGrey",             "#A9A9A9");
            Variables.Add("DarkGreen",            "#006400");
            Variables.Add("DarkKhaki",            "#BDB76B");
            Variables.Add("DarkMagenta",          "#8B008B");
            Variables.Add("DarkOliveGreen",       "#556B2F");
            Variables.Add("DarkOrange",           "#FF8C00");
            Variables.Add("DarkOrchid",           "#9932CC");
            Variables.Add("DarkRed",              "#8B0000");
            Variables.Add("DarkSalmon",           "#E9967A");
            Variables.Add("DarkSeaGreen",         "#8FBC8F");
            Variables.Add("DarkSlateBlue",        "#483D8B");
            Variables.Add("DarkSlateGray",        "#2F4F4F");
            Variables.Add("DarkSlateGrey",        "#2F4F4F");
            Variables.Add("DarkTurquoise",        "#00CED1");
            Variables.Add("DarkViolet",           "#9400D3");
            Variables.Add("DeepPink",             "#FF1493");
            Variables.Add("DeepSkyBlue",          "#00BFFF");
            Variables.Add("DimGray",              "#696969");
            Variables.Add("DimGrey",              "#696969");
            Variables.Add("DodgerBlue",           "#1E90FF");
            Variables.Add("FireBrick",            "#B22222");
            Variables.Add("FloralWhite",          "#FFFAF0");
            Variables.Add("ForestGreen",          "#228B22");
            Variables.Add("Fuchsia",              "#FF00FF");
            Variables.Add("Gainsboro",            "#DCDCDC");
            Variables.Add("GhostWhite",           "#F8F8FF");
            Variables.Add("Gold",                 "#FFD700");
            Variables.Add("GoldenRod",            "#DAA520");
            Variables.Add("Gray",                 "#808080");
            Variables.Add("Grey",                 "#808080");
            Variables.Add("Green",                "#008000");
            Variables.Add("GreenYellow",          "#ADFF2F");
            Variables.Add("HoneyDew",             "#F0FFF0");
            Variables.Add("HotPink",              "#FF69B4");
            Variables.Add("IndianRed",            "#CD5C5C");
            Variables.Add("Indigo",               "#4B0082");
            Variables.Add("Ivory",                "#FFFFF0");
            Variables.Add("Khaki",                "#F0E68C");
            Variables.Add("Lavender",             "#E6E6FA");
            Variables.Add("LavenderBlush",        "#FFF0F5");
            Variables.Add("LightCyan",            "#E0FFFF");
            Variables.Add("LawnGreen",            "#7CFC00");
            Variables.Add("LemonChiffon",         "#FFFACD");
            Variables.Add("LightBlue",            "#ADD8E6");
            Variables.Add("LightCoral",           "#F08080");
            Variables.Add("LightGoldenRodYellow", "#FAFAD2");
            Variables.Add("LightGray",            "#D3D3D3");
            Variables.Add("LightGrey",            "#D3D3D3");
            Variables.Add("LightGreen",           "#90EE90");
            Variables.Add("LightPink",            "#FFB6C1");
            Variables.Add("LightSalmon",          "#FFA07A");
            Variables.Add("LightSeaGreen",        "#20B2AA");
            Variables.Add("LightSkyBlue",         "#87CEFA");
            Variables.Add("LightSlateGray",       "#778899");
            Variables.Add("LightSlateGrey",       "#778899");
            Variables.Add("LightSteelBlue",       "#B0C4DE");
            Variables.Add("LightYellow",          "#FFFFE0");
            Variables.Add("Lime",                 "#00FF00");
            Variables.Add("LimeGreen",            "#32CD32");
            Variables.Add("Linen",                "#FAF0E6");
            Variables.Add("Magenta",              "#FF00FF");
            Variables.Add("Maroon",               "#800000");
            Variables.Add("MediumAquaMarine",     "#66CDAA");
            Variables.Add("MediumBlue",           "#0000CD");
            Variables.Add("MediumOrchid",         "#BA55D3");
            Variables.Add("MediumPurple",         "#9370DB");
            Variables.Add("MediumSeaGreen",       "#3CB371");
            Variables.Add("MediumSlateBlue",      "#7B68EE");
            Variables.Add("MediumSpringGreen",    "#00FA9A");
            Variables.Add("MediumTurquoise",      "#48D1CC");
            Variables.Add("MediumVioletRed",      "#C71585");
            Variables.Add("MidnightBlue",         "#191970");
            Variables.Add("MintCream",            "#F5FFFA");
            Variables.Add("MistyRose",            "#FFE4E1");
            Variables.Add("Moccasin",             "#FFE4B5");
            Variables.Add("NavajoWhite",          "#FFDEAD");
            Variables.Add("Navy",                 "#000080");
            Variables.Add("OldLace",              "#FDF5E6");
            Variables.Add("Olive",                "#808000");
            Variables.Add("OliveDrab",            "#6B8E23");
            Variables.Add("Orange",               "#FFA500");
            Variables.Add("OrangeRed",            "#FF4500");
            Variables.Add("Orchid",               "#DA70D6");
            Variables.Add("PaleGoldenRod",        "#EEE8AA");
            Variables.Add("PaleGreen",            "#98FB98");
            Variables.Add("PaleTurquoise",        "#AFEEEE");
            Variables.Add("PaleVioletRed",        "#DB7093");
            Variables.Add("PapayaWhip",           "#FFEFD5");
            Variables.Add("PeachPuff",            "#FFDAB9");
            Variables.Add("Peru",                 "#CD853F");
            Variables.Add("Pink",                 "#FFC0CB");   
            Variables.Add("Plum",                 "#DDA0DD");
            Variables.Add("PowderBlue",           "#B0E0E6");
            Variables.Add("Purple",               "#800080");
            Variables.Add("RebeccaPurple",        "#663399");
            Variables.Add("Red",                  "#FF0000");
            Variables.Add("RosyBrown",            "#BC8F8F");
            Variables.Add("RoyalBlue",            "#4169E1");
            Variables.Add("SaddleBrown",          "#8B4513");
            Variables.Add("Salmon",               "#FA8072");
            Variables.Add("SandyBrown",           "#F4A460");
            Variables.Add("SeaGreen",             "#2E8B57");
            Variables.Add("SeaShell",             "#FFF5EE");
            Variables.Add("Sienna",               "#A0522D");
            Variables.Add("Silver",               "#C0C0C0");
            Variables.Add("SkyBlue",              "#87CEEB");
            Variables.Add("SlateBlue",            "#6A5ACD");
            Variables.Add("SlateGray",            "#708090");
            Variables.Add("SlateGrey",            "#708090");
            Variables.Add("Snow",                 "#FFFAFA");
            Variables.Add("SpringGreen",          "#00FF7F");
            Variables.Add("SteelBlue",            "#4682B4");
            Variables.Add("Tan",                  "#D2B48C");
            Variables.Add("Teal",                 "#008080");
            Variables.Add("Thistle",              "#D8BFD8");
            Variables.Add("Tomato",               "#FF6347");
            Variables.Add("Turquoise",            "#40E0D0");
            Variables.Add("Violet",               "#EE82EE");
            Variables.Add("Wheat",                "#F5DEB3");
            Variables.Add("White",                "#FFFFFF");
            Variables.Add("WhiteSmoke",           "#F5F5F5");
            Variables.Add("Yellow",               "#FFFF00");
            Variables.Add("YellowGreen",          "#9ACD32");
            Variables.Add("Transparent",          "#ffffff00");
            Variables.Add("Clear",                "#ffffff00");
            Dictionary<string, string> nv = new Dictionary<string, string>();
            foreach (var item in Variables) nv[item.Key.ToLower()] = item.Value;

            Variables = nv;
        }
        public static string GetVariable(string key) {
            return Variables[key.Remove(0,1)];
        }


        public static string[] formatCode(string[] src) {
            return formatCode(string.Join("", src));
        }
        public static string[] formatCode(string src) {
            return Regex
                .Split(Regex.Replace(src, @"\/\*.*?\*\/", ""), @"(?<!\^);(?![^\{\}]*\})")
                .Where(l => l.Length>0)
                .Select(l => l =
                    (l[0]==cmdPrefix
                        ?l
                        :Regex.Replace(l, spaceMatch, "$1")
                    ).Replace("^;", ";")
            ).ToArray();
        }
        public static Font LoadFont(string path, int targetFontSize = 64)
        {
            Font ret;
            int[] arr = new int[1600];
            for (int i=0; i < arr.Length; i++) arr[i] = i;
            ret = Raylib.LoadFontEx(path, targetFontSize, arr, arr.Length);
            Raylib.SetTextureFilter(ret.texture, TextureFilter.TEXTURE_FILTER_BILINEAR);
            return ret;
        }
        public static Font GetFont(string f)
        {
            Font F;
            if (!Fonts.TryGetValue(f, out F)) F = Raylib.GetFontDefault();
            return F;
        }
    
        public static void LoadScript(string path)
        {
            StaticStyles.Clear();
            Variables.Clear();
            // formating the code into one statement per line
            Path = path;
            string colapsedCode = string.Join("",File.ReadAllLines(path));
            FromatedCode = formatCode(colapsedCode).ToList();
            ParseStyle();

        }
        public static void ParseStyle()
        {
            StaticStyles.Clear();
            DynamicStyles.Clear();
            Variables.Clear();
            AddCols();
            // creating variables
            for (int i = 0; i < FromatedCode.Count; i++)
            {
                string line = FromatedCode[i];
                if (line[0] == cmdPrefix)
                {
                    string[] cmd = line.Remove(0,1).Split(" ", 2);
                    if (cmd[0] == "importscript")
                    {
                        string[] importedCode = formatCode(File.ReadAllLines(cmd[1]));
                        
                        FromatedCode.RemoveAt(i);
                        for (int l = 0; l < importedCode.Length; l++)
                        {
                            FromatedCode.Insert(i+l, importedCode[l]);
                        }
                        cmd = FromatedCode[i].Remove(0,1).Split(" ", 2);
                    }
                    if (cmd[0] == "setconst" || cmd[0] == "setvar")
                    {
                        string[] args = cmd[1].Split(" ", 2);
                        string[] fmt = formatCode(args[1]);
                        if (args.Length < 2) continue;
                        if (Variables.ContainsKey(args[0])) Variables.Add(args[0], Regex.Replace(args[1], spaceMatch, "$1"));
                        else Variables[args[0]] = Regex.Replace(args[1], spaceMatch, "$1");
                    }
                    if (cmd[0] == "setbackground")
                    {
                        BGCol = GUITools.HexToRGB(cmd[1]);
                    }
                    if (cmd[0] == "setnamespace")
                    {
                        Namespace = cmd[1];
                    }
                    if (cmd[0] == "importfont")
                    {
                        string[] args = cmd[1].Split(" ", 2);
                        string name = System.IO.Path
                            .GetFileName(args[1])
                            .Replace(' ',  '-')
                            .Replace(".ttf", "")
                            .ToLower();
                        if (!Fonts.ContainsKey(name)) Fonts.Add(name, LoadFont(args[1], (int)GUITools.Eval(args[0])));
                    }
                }
            }
            #region search & replace consts
            // replacing consts refrences in variables with real values
            while (Variables.Where(v => v.Value.Contains(varPrefix) && Variables.ContainsKey(Regex.Match(v.Value, string.Format(@"\{0}(\w)*", varPrefix)).Value.Remove(0,1))).Count() > 0)
                foreach (var item in Variables)
                {
                    if (!item.Value.Contains(varPrefix)) continue;
/*
                    Match match = Regex.Match(item.Value, string.Format(@"\{0}(\w)*", varPrefix));
                    Variables[item.Key] = item.Value.Replace(match.Value, GetVariable(match.Value));*/
                    
                    MatchCollection matches = Regex.Matches(Variables[item.Key], string.Format(@"\{0}(\w)*", varPrefix));
                    foreach (Match match in matches)
                    {
                        if (match.Value==null || !Variables.ContainsKey(match.Value.Remove(0,1))) continue;
                        Variables[item.Key] = Variables[item.Key].Replace(match.Value, GetVariable(match.Value));
                    } 
                }
            
            // replacing varibale refrences in code with real values
            
            while (FromatedCode.Where(v => v.Contains(varPrefix) && Variables.ContainsKey(Regex.Match(v, string.Format(@"\{0}(\w)*", varPrefix)).Value.Remove(0,1))).Count() > 0)
                for (int i = 0; i < FromatedCode.Count; i++)
                {
                    if (!FromatedCode[i].Contains(varPrefix)) continue;

                    MatchCollection matches = Regex.Matches(FromatedCode[i], string.Format(@"\{0}(\w)*", varPrefix));
                    foreach (Match match in matches)
                    {
                        if (match.Value==null || !Variables.ContainsKey(match.Value.Remove(0,1))) continue;
                        FromatedCode[i] = FromatedCode[i].Replace(match.Value, GetVariable(match.Value));
                    } 
                }
            #endregion
            // creating style classes from the formated code
            var FFF = FromatedCode;
            foreach (string l in FromatedCode)
            {
                string line = l;
                Dictionary<string, string> localVars = new Dictionary<string, string>();
                Variables.Concat(localVars);
                StaticStyle style = new StaticStyle();

                string[] splitLine = Regex.Split(line, @"{|;|}").Where(l=>l.Length>0).ToArray();
                if (splitLine.Length < 2) continue;
                
            
                // replacing varibale refrences in code with real values

                for (int i = 1; i < splitLine.Length; i++)
                {
                    string[] v = splitLine[i].Replace(";","").Split(":");
                    if (v.Length != 2) continue;

                    switch (v[0].ToLower())
                    {
                        case "background-col": style.backgroundCol = GUITools.HexToRGB(v[1]); continue;
                        case "foreground-col": style.foregroundCol = GUITools.HexToRGB(v[1]); continue;

                        case "border-col":   style.borderCol   = GUITools.HexToRGB(v[1]); continue;
                        case "border-width": style.borderWidth = GUITools.Eval(v[1]);     continue;

                        case "rect":   style.rect   = GUITools.StringToRect(v[1]);   continue;
                        case "parent": style.parent = GUITools.StringToRect(v[1]);   continue;
                        case "anchor": style.anchor = GUITools.StringToAnchor(v[1]); continue;

                        case "text":         style.text        = GUITools.StringToString(v[1]); continue;
                        case "text-align":   style.textAlign   = GUITools.StringToAnchor(v[1]); continue;

                        case "font":         style.font        = GetFont(v[1]);       continue;
                        case "font-size":    style.fontSize    = GUITools.Eval(v[1]); continue;
                        case "font-spacing": style.fontSpacing = GUITools.Eval(v[1]); continue;

                        case "call-cs":
                            string[] total = v[1].Split(".");
                            string _namespace = "";
                            string _class     = "";
                            string _func      = "";
                            if (total.Length == 2)
                            {
                                _namespace = Namespace;
                                _class = total[0];
                                _func = total[1];
                            }
                            if (total.Length == 3)
                            {
                                _namespace = total[0];
                                _class = total[1];
                                _func = total[2];
                            }
                            if (_func.Contains("("))
                            {
                                string[] function = 
                                {
                                    Regex.Replace(_func, @"\(.*?\)", ""),
                                    Regex.Replace(Regex.Match(_func, @"\(.*?\)").Value, @"\(|\)", "")
                                };


                            }
                            else style.functions.Add(Type.GetType(_namespace+"."+_class).GetMethod(_func));
                            continue;

                        case "var":
                            string[] nv = v[1].Split("=");
                            localVars.Add(nv[0], nv[1]);
                            Variables.Add(nv[0], nv[1]);
                            
                            string matchString = @"\"+varPrefix+@"\b"+nv[0]+@"\b";
                            while (splitLine.Where(v => Regex.IsMatch(v, matchString)).Count() > 0)
                            {
                                for (int a = i; a < splitLine.Length; a++)
                                {
                                    if (!Regex.IsMatch(splitLine[a], matchString)) continue;

                                    Match match = Regex.Match(splitLine[a], matchString);
                                    splitLine[a] = Regex.Replace(splitLine[a], matchString, GetVariable(varPrefix+nv[0])); //splitLine[a].Replace(varPrefix+nv[0], GetVariable(varPrefix+nv[0]));
                                }
                            }
                            continue;
                        default: continue;
                    }
                }
                if (!StaticStyles.ContainsKey(splitLine[0]) && !splitLine[0].Contains(cmdPrefix)) StaticStyles.Add(splitLine[0], style);
                foreach (var v in localVars) Variables.Remove(v.Key);
                localVars.Clear();
            }
            Dictionary<string, List<string>> ns = new Dictionary<string, List<string>>(); // new styles
            foreach (var s in StaticStyles)
            {
                if (!s.Key.Contains(":")) ns.Add(s.Key, new List<string>());
                else 
                {
                    string[] options = s.Key.Split(":", 2);
                    if (ns.ContainsKey(options[0])) ns[options[0]].Add(options[1]);
                }
            }

            foreach (var s in ns)
            {
                DynamicStyles.Add(s.Key, new DynamicStyle(StaticStyles[s.Key], new Dictionary<string, StaticStyle>()));
                foreach (var i in s.Value) DynamicStyles[s.Key].styles.Add(i, StaticStyles[s.Key+":"+i]);
            }
            var CS = DynamicStyles;
        }
    }
}