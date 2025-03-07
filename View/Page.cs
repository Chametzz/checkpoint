public class Window {
    public List<Page> pages;
    public Stack<Page> log;
    public Window() {
        pages = [];
        log = [];
    }
    public Page InsertPage(string title) {
        pages.Add(new Page(this, title));
        return pages[^1];
    }
    public Page InsertPage(string title, Action<Page> toLoad) {
        pages.Add(new Page(this, title, toLoad));
        return pages[^1];
    }
    public Page InsertPage(Page page) {
        pages.Add(page);
        return pages[^1];
    }

    public void LoadPage(Page page) {//Carga otra página
        log.Push(page);
        log.Peek().Load();
    }

    public void ReLoadPage() {//Recarga la ágina actual
        log.Peek().Load();
    }

    public void RefreshPage() {//Te dibuja sin cambiar los valores que estaban
        log.Peek().Draw();
    }
    public void BackLoadPage() {//Te regresa a la página anterior y la carga
        log.Pop();
        log.Peek().Load();
    }
    public void BackRefreshPage() {//Te regresa a la página anterior y solo la vuelve a dibujar
        log.Pop();
        log.Peek().Draw();
    }

    public void ReplacePage(Page page) {//Reemplaza la página actual por otra
        log.Pop();
        LoadPage(page);
    }
    
    public void Execute() {
        LoadPage(pages[0]);
        bool running = true;
        do{
            ConsoleKeyInfo entry = Console.ReadKey(true);

            //Regresar a la pantalla anterior
            if(entry.Key == ConsoleKey.Escape) {
                if(log.Count > 1) {
                    log.Pop();
                    log.Peek().Load();
                    continue;
                }
            }

            //Ir a otras pantallas
            foreach (var link in log.Peek().links) {
                if(entry.Key == link.key) {
                    LoadPage(link.Page);
                    continue;
                }
            }
            
            if(entry.Key == ConsoleKey.UpArrow) {
                log.Peek().SetCursorID(log.Peek().cursorID - 1);
            }
            if(entry.Key == ConsoleKey.DownArrow) {
                log.Peek().SetCursorID(log.Peek().cursorID + 1);
            }

            //Checar interacciones con la pantalla
            if(log.Peek().cursorID > -1) {
                log.Peek().interactives[log.Peek().cursorID].input.Capture(entry);
            }
        } while(running);
    }
}
public class Page {
    public Window wind;
    public string title;
    public Action<Page>? toLoad;
    public List<Label> labels;
    public List<Link> links;
    public int cursorID = -1;
    public List<(Label input, int line)> interactives;
    //public Dictionary<string, object> data;
    public Dictionary<string, string> refs;
    public Page(Window wind, string title) {
        this.wind = wind;
        this.title = title;
        this.toLoad = null;
        labels = [];
        links = [];
        interactives = [];
        refs = [];
        SetCursorID(-1);
    }
    public Page(Window wind, string title, Action<Page> toLoad) {
        this.wind = wind;
        this.title = title;
        this.toLoad = toLoad;
        labels = [];
        links = [];
        interactives = [];
        refs = [];
        SetCursorID(-1);
    }
    public T InsertLabel<T>(string content) where T : Label, new() {
        T label = new();
        label.page = this;
        label.parent = null;
        label.content = content;
        label.Start();
        labels.Add(label);
        return label;
    }
    public T InsertLabel<T>(string content, params (string name, string value)[] properties) where T : Label, new() {
        T label = new();
        label.page = this;
        label.parent = null;
        label.content = content;
        label.properties = [];
        foreach (var (name, value) in properties) {
            label.properties.Add(name, value);
        }
        label.Start();
        labels.Add(label);
        return label;
    }
    public Label? SearchLabel(string id) {
        foreach (var label in labels) {
            if(label.GetProperty("id") == id) {
                return label;
            }
            if(label.childs != null && label.childs.Count > 0) {
                Label? child = SearchLabel(id, label.childs);
                if (child != null) return child;
            }
        }
        return null;
    }
    public T? SearchLabel<T>(string id) where T : Label {
            foreach (var label in labels) {
            if (label.GetProperty("id") == id && label is T) {
                return (T)label;
            }

            if (label.childs != null && label.childs.Count > 0) {
                T? child = SearchLabel<T>(id, label.childs);
                if (child != null) return child;
            }
        }
        return null;
    }
    Label? SearchLabel(string id, List<Label> labels) {
        foreach (var label in labels) {
            if(label.GetProperty("id") == id) {
                return label;
            }
            if(label.childs != null && label.childs.Count > 0) {
                Label? child = SearchLabel(id, label.childs);
                if (child != null) return child;
            }
        }
        return null;
    }
    public T? SearchLabel<T>(string id, List<Label> childList) where T : Label {
        foreach (var label in childList) {
            if (label.GetProperty("id") == id && label is T) {
                return (T)label;
            }

            if (label.childs != null && label.childs.Count > 0) {
                T? child = SearchLabel<T>(id, label.childs);
                if (child != null) return child;
            }
        }
        return null;
    }
    public void InsertLink(ConsoleKey key, Page page) {
        links.Add(new Link(key, page.title, page));
    }
    public void InsertLink(ConsoleKey key, Page page, string name) {
        links.Add(new Link(key, name, page));
    }
    
    public void SetCursorID(int newID) {
        if(cursorID >= 0 && interactives.Count > cursorID) {
            Console.CursorTop = interactives[cursorID].line;
            interactives[cursorID].input.Off();
        } else {
            cursorID = -1;
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;
        }
        int n = newID + 1;
        int total = interactives.Count + 1;
        cursorID = ((n % total) + total) % total - 1;//Para que pueda rondar entre -1 y total
        if(cursorID < 0) {
            Console.CursorVisible = false;
            Console.SetCursorPosition(0, 0);
            //Es por un bug del cursor en el powershell siempre habra una H en la posición 0, 0
            ConsoleColor color = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.Write("H");
            Console.ForegroundColor = color;
            Console.CursorLeft = 0;
        } else {
            Console.CursorTop = interactives[cursorID].line;
            interactives[cursorID].input.On();
            Console.CursorVisible = true;
        }
    }
    public void SetRef(string key, string value) {
        if(refs.ContainsKey(key)) {
            refs[key] = value;
        } else {
            refs.Add(key, value);
        }
    }
    public string GetRef(string key) {
        if(refs.ContainsKey(key)) {
            return refs[key];
        } else {
            return "";
        }
    }
    public void Draw() {
        interactives = [];
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Page[] historial = wind.log.ToArray();
        Console.Write("Historial: ");
        for (int i = historial.Length - 1; i >= 0; i--) {
            if (i > 0) {
                Console.Write($"{historial[i].title} -> ");
            } else {
                Console.Write(historial[i].title);
            }
        }
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("╔" + new string('═', Console.WindowWidth - 2) + "╗"); // Línea decorativa
        string centeredTitle = title.PadLeft((Console.WindowWidth + title.Length) / 2).PadRight(Console.WindowWidth - 2);
        Console.Write("║");
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write(centeredTitle);
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine("║");
        Console.WriteLine("╚" + new string('═', Console.WindowWidth - 2) + "╝"); // Línea separadora

        // Sección: Enlaces interactivos
        
        Console.Write("Enlaces: ");
        foreach (var link in links) {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("[");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(link.key);
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write($"] <{link.name}>   ");
        }
        Console.WriteLine();
        Console.WriteLine(new string('-', Console.WindowWidth)); // Línea separadora
        //Labels
        foreach (Label label in labels) {
            label.Show();
        }
        SetCursorID(cursorID);
        Console.ForegroundColor = ConsoleColor.DarkGray;
    }

    public void Load() {
        toLoad?.Invoke(this);
        if(this == wind.log.Peek()) {
            Draw();
            if(toLoad != null) {
                SetCursorID(-1);
            }
        }
    }
}

public struct Link {
    public ConsoleKey key;
    public string name;
    public Page Page;
    public Link(ConsoleKey key, string name, Page Page) {
        this.key = key;
        this.name = name;
        this.Page = Page;
    }
}

public class Label {
    public Page? page;
    public Label? parent;
    public string content = "";
    public Dictionary<string, string> properties = [];
    public List<Label> childs = [];
    public virtual void Start() {}
    public T InsertChild<T>(string content) where T : Label, new() {
        T label = new();
        label.page = page;
        label.parent = this;
        label.content = content;
        label.Start();
        childs.Add(label);
        return label;
    }
    public T InsertChild<T>(string content, params (string name, string value)[] properties) where T : Label, new() {
        T label = new();
        label.page = page;
        label.parent = this;
        label.content = content;
        label.properties = [];
        foreach (var (name, value) in properties) {
            label.properties.Add(name, value);
        }
        label.Start();
        childs.Add(label);
        return label;
    }
    public void SetProperty(string name, string value) {
        if(properties.ContainsKey(name)) {
            properties[name] = value;
        } else {
            properties.Add(name, value);
        }
    }
    public string GetProperty(string name) {
        if(properties.ContainsKey(name)) {
            return properties[name];
        } else {
            return "";
        }
    }
    public T GetProperty<T>(string name) where T : struct{
        if (properties.ContainsKey(name)) {
            try {
                return (T)Convert.ChangeType(properties[name], typeof(T));
            } catch {
                return default;
            }
        } else {
            return default;
        }
    }

    public virtual void Show() {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(page != null && GetProperty("ref") == ""? page.refs[properties["ref"]]: content);
        foreach (Label child in childs) {
                child.Show();
            }
    }
    
    public virtual void Capture(ConsoleKeyInfo entry) {}
    public virtual void On() {}
    public virtual void Off() {}
}

public class Input : Label {
    public Action<Label, Page>? action = null;
    public override void Start() {
        if(!properties.ContainsKey("value")) properties.Add("value", "");
        if(!properties.ContainsKey("type")) properties.Add("type", "");
        switch (properties["type"]){
            case "number":
                if(!properties.ContainsKey("decimal")) properties.Add("decimal", "false");
                if(properties["value"].Contains('.')) {
                    SetProperty("point", "true");
                } else {
                    SetProperty("point", "false");
                }
                break;
        }
    }
    public override void Show() {
        if(GetProperty("hide") != "true") {
            if(properties["type"] != "submit") {
                Console.Write(page != null && GetProperty("ref") != ""? page.refs[properties["ref"]]: content);
                if(GetProperty("required") == "true") {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(" *");
                } else {
                    Console.WriteLine("");
                }
            }
            page?.interactives.Add((this, Console.CursorTop));
            Console.ForegroundColor = ConsoleColor.DarkGray;
            switch (properties["type"])
            {
                case "password":
                    Console.WriteLine(new string('*', properties["value"].Length));
                    break;
                case "select":
                    if(properties["value"] == "") {
                        Console.WriteLine("< ENTER PARA SELECCIONAR >");
                    } else {
                        Console.WriteLine("< " + properties["value"] + " >");
                    }
                    break;
                case "submit":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(page != null && GetProperty("ref") != ""? page.refs[properties["ref"]]: content);
                    break;
                default:
                    Console.WriteLine(properties["value"]);
                    break;
            }
        }
        Console.ForegroundColor = ConsoleColor.White;
    }
    public override void On() {
        switch(GetProperty("type")??"") {
            case "select":
                Console.BackgroundColor = ConsoleColor.DarkGray;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.CursorLeft = 0;
                string option = "";
                if(properties["value"] == "") {
                    option = "< ENTER PARA SELECCIONAR >";
                } else {
                    option = "< " + properties["value"] + " >";
                }
                Console.Write(option);
                Console.CursorLeft = option.Length;
                break;
            case "submit":
                Console.BackgroundColor = ConsoleColor.Yellow;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.CursorLeft = 0;
                Console.Write(content);
                Console.CursorLeft = content.Length;
                break;
            default:
                Console.CursorLeft = Console.CursorLeft = properties["value"].Length;
                break;
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkGray;
    }
    public override void Off() {
        switch(GetProperty("type")??"") {
            case "select":
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.CursorLeft = 0;
                string option = "";
                if(properties["value"] == "") {
                    option = "< ENTER PARA SELECCIONAR >";
                } else {
                    option = "< " + properties["value"] + " >";
                }
                Console.Write(option);
                Console.CursorLeft = option.Length;
                break;
            case "submit":
                Console.BackgroundColor = ConsoleColor.Black;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.CursorLeft = 0;
                Console.Write(content);
                Console.CursorLeft = content.Length;
                break;
        }
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkGray;
    }

    public override void Capture(ConsoleKeyInfo entry) {
        string type = GetProperty("type")??"";
        switch (type) {
            case "text":
                if (entry.Key == ConsoleKey.Backspace && Console.CursorLeft > 0) {//Borrar
                    Backspace();
                }
                if(!char.IsControl(entry.KeyChar)) {
                    properties["value"] += entry.KeyChar;
                    Console.Write(entry.KeyChar);
                    Console.CursorLeft = properties["value"].Length;
                }
                NextEnter(entry);
                break;
            case "number":
                if (entry.Key == ConsoleKey.Backspace && Console.CursorLeft > 0) {//Borrar
                    char delete = Backspace();
                    if (delete == '.') SetProperty("point", "false");
                }
                if(GetProperty("decimal") != "true") {
                    if(char.IsDigit(entry.KeyChar)) {
                        properties["value"] += entry.KeyChar;
                        Console.Write(entry.KeyChar);
                        Console.CursorLeft = properties["value"].Length;
                    }
                } else {
                    if(char.IsDigit(entry.KeyChar) || (entry.KeyChar == '.' && properties["point"] != "true")) {
                        if(entry.KeyChar == '.') SetProperty("point", "true");
                        properties["value"] += entry.KeyChar;
                        Console.Write(entry.KeyChar);
                        Console.CursorLeft = properties["value"].Length;
                    }
                }
                NextEnter(entry);
                break;
            case "password":
                if (entry.Key == ConsoleKey.Backspace && Console.CursorLeft > 0) {//Borrar
                    Backspace();
                }
                if(!char.IsControl(entry.KeyChar)) {
                    properties["value"] += entry.KeyChar;
                    Console.Write("*");
                    Console.CursorLeft = properties["value"].Length;
                }
                NextEnter(entry);
                break;
            case "select":
                if (entry.Key == ConsoleKey.Enter && page != null) {
                    Page selection = new Page(page.wind, "ELIJA UNA OPCIÓN");
                    selection.InsertLabel<Label>("Elija una de las siguientes opciones: ");
                    foreach (var child in childs) {
                        selection.InsertLabel<Button>(child.content).action = () => {
                            properties["value"] = child.properties["value"];
                            if(action != null) {
                                action(this, page);
                            } else {
                                page.wind.BackRefreshPage();
                            }
                        };
                    }
                    NextEnter(entry);
                    page.wind.LoadPage(selection);
                }
                
                break;
            case "submit":
                if (entry.Key == ConsoleKey.Enter) {
                    if (parent != null && parent.GetType().ToString() == "Form") {
                        Form form = (Form)parent;
                        Dictionary<string, string> data = [];
                        foreach (Label child in parent.childs) {
                            if(child.properties.ContainsKey("name") && child.properties.ContainsKey("value")) {
                                data.Add(child.properties["name"], child.properties["value"]);
                                if(child.GetProperty("required") == "true" && child.properties["value"] == "" && !form.warnings.Contains("Rellena todos los campos requeridos.")) {
                                    form.warnings.Enqueue("Rellena todos los campos requeridos.");
                                }
                            }
                        }

                        if(form.warnings.Count <= 0) {
                            form.action(form, data);
                        } else {
                            page?.wind.RefreshPage();
                        }
                    }
                }
                break;
        }
    }
    char Backspace() {
        char del = properties["value"][^1];
        properties["value"] = properties["value"][..^1];
        Console.CursorLeft -= 1;
        Console.Write(" ");
        Console.CursorLeft -= 1;
        return del;
    }
    
    void NextEnter(ConsoleKeyInfo entry) {
        if(entry.Key == ConsoleKey.Enter) {
            page?.SetCursorID(page.cursorID + 1);
        }
    }
}

public class Button : Label {
    public Action action = () => {};
    public override void Start() {
        //SetProperty("value", content);
    }
    public override void On() {
        Console.BackgroundColor = ConsoleColor.Yellow;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.CursorLeft = 0;
        Console.Write(content);
        Console.CursorLeft = content.Length;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkGray;
    }
    public override void Off() {
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.CursorLeft = 0;
        Console.Write(content);
        Console.CursorLeft = content.Length;
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.DarkGray;
    }
    public override void Show() {

        Console.ForegroundColor = ConsoleColor.Yellow;
        if(GetProperty("hide") != "true") {
            Console.Write(content);
            page?.interactives.Add((this, Console.CursorTop));
            Console.WriteLine("");
            foreach (Label child in childs) {
                child.Show();
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    public override void Capture(ConsoleKeyInfo entry)
    {
        if (entry.Key == ConsoleKey.Enter) {
            action();
        }
    }
}
public class Form : Label {
    public Action<Form, Dictionary<string, string>> action = (form, data) => {};
    public Queue<string> warnings = [];
    public override void Show() {
        Console.ForegroundColor = ConsoleColor.White;
        if(GetProperty("hide") != "true") {
            Console.WriteLine(page != null && GetProperty("ref") != ""? page.refs[properties["ref"]]: content);
            foreach (Label child in childs) {
                child.Show();
            }
            Console.ForegroundColor = ConsoleColor.Red;
            while (warnings.Count > 0) {
                Console.WriteLine(warnings.Dequeue());
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
    public void SetWarning(string warning) {
        warnings.Enqueue(warning);
    }
    public void SetAction(Action<Form, Dictionary<string, string>> action) {
        this.action = action;
    }
}
public class Table : Label {
    public string[,] table = new string[0,0];
    public override void Start() {
        if(!properties.ContainsKey("tab")) properties.Add("tab", "8");
    }

    public override void Show() {
        if(GetProperty("hide") != "true") {
            int[] columnWidths = new int[table.GetLength(0)];
            int[] rowHeights = new int[table.GetLength(1)];

            // Determinar el ancho de cada columna y la altura de cada fila
            for (int x = 0; x < table.GetLength(0); x++) {
                columnWidths[x] = int.Parse(properties["tab"]);
                for (int y = 0; y < table.GetLength(1); y++) {
                    string data = table[x, y];

                    // Ajustar el ancho de la columna
                    int maxLineLength = data.Split("\\n").Max(line => line.Length);
                    columnWidths[x] = Math.Max(columnWidths[x], maxLineLength + 2); // +2 por el espacio adicional

                    // Ajustar la altura de la fila según los saltos de línea
                    int lineCount = data.Split("\\n").Length;
                    rowHeights[y] = Math.Max(rowHeights[y], lineCount);
                }
            }

            // Construir los bordes de la tabla
            string topBorder = "┌" + string.Join("┬", columnWidths.Select(w => new string('─', w))) + "┐";
            string middleBorder = "├" + string.Join("┼", columnWidths.Select(w => new string('─', w))) + "┤";
            string bottomBorder = "└" + string.Join("┴", columnWidths.Select(w => new string('─', w))) + "┘";

            // Mostrar la tabla
            Console.WriteLine(topBorder);

            for (int y = 0; y < table.GetLength(1); y++) {
                for (int line = 0; line < rowHeights[y]; line++) {
                    // Comienza una nueva línea de la tabla
                    Console.Write("│");
                    
                    for (int x = 0; x < table.GetLength(0); x++) {
                        string[] lines = table[x, y].Split("\\n");
                        string currentLine = line < lines.Length ? lines[line].Replace("\r", "") : ""; // Si no hay más líneas, dejar vacío
                        // Formatear la celda con el contenido
                        string content = " " + currentLine + " ";
                        content = content.PadRight(columnWidths[x]);
                        
                        Console.Write(content + "│");
                    }
                    Console.WriteLine();
                }

                // Dibujar la línea de separación
                if (y < table.GetLength(1) - 1) {
                    Console.WriteLine(middleBorder); // Separar las filas con líneas
                }
            }

            Console.WriteLine(bottomBorder);
        }
    }
}