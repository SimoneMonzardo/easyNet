# C# Code Style

Per far sì che il codice sia scritto tutto nello stesso formato, in modo che sembri scritto dalla stessa persona, e che i reviewers possano concentrarsi sul codice e non sul suo formato, adottiamo queste guidelines:

#### I nomi delle variabili, dei metodi e delle classi dovranno essere in INGLESE

<h3>1. Convenzioni per i nomi delle classi</h3>

- Dare nomi sensati che rappresentino l'oggetto/classe
- Usare la notazione `PascalCase` per le classi
- Usare la notazione `PascalCase` preceduta da una I per le interfacce
- Usare la notazione `PascalCase` per le Struct
- Usare la notazione `PascalCase` per le funzioni/metodi
- Usare la notazione `PascalCase` per gli Enum
- Usare il suffisso `Model` per le classi del model
- Usare il prefisso `Base` per le classi astratte
- Usare il suffisso `Utility` per le classi di aiuto (tipo per inviare le mail)

<h3>2. Convenzioni per i nomi delle variabili</h3>

- Usare la notazione `PascalCase` per le properties pubbliche
- Usare la notazione `PascalCase` per le properties e i campi statici
- Usare la notazione `_camelCase` per i campi privati
- Usare la notazione `camelCase` per le variabili presenti nella firme dei metodi
- Usare la notazione `camelCase` per le variabili presenti nel corpo dei metodi
- Usare la notazione `UPPER_CASE` per le costanti
- Usare i tipi predefiniti `int`, `long`, `string` al posto di `Int32`, `Int64`, `String`

<h3>3. Leggibilità del codice</h3>

- Allineare le graffe su una nuova linea
```
    if (true)
    {
    
    }
```
- Usare le graffe sempre per gli `if`, i `for` e i `foreach` (anche se il corpo è di una sola riga)
- Usare le direttive `#region` se possibile
- Usare i prefissi `Is`, `Has`, `Have`, `Any`, `Can` o simili per i nomi delle variabili booleane e dei metodi se applicabile
- Non usare i suffissi/prefissi `Enum` o `Flag` per i nomi degli `Enum`
- Evitare di scrivere righe eccessivamente lunghe (+ 150 caratteri, dividerele se possibile)
- Dividere le condizioni di `if`, `while`, `do-while` e simili su più righe se possibile
```
    if (true &&
        true &&
        false)
    {

    }
```
- Dichiarare tutte le costanti in un unico file, divise per categoria (Aggiungere un commento sopra la categoria per identificarla)
- Aggiungere `abstract` o `sealed` alla dichiarazione delle classi se applicabile
- Le interfacce vanno documentate
- Usare i tipi più astratti dentro ai metodi/classi
- Definizione di numeri elevati: `1_000_000`
- Usare il principio della singola responsabilità nella definizione delle classi e dei metodi
- Non scrivere più di una classe per file
- Evitare di usare `/* */` per definire delle regioni di commento
- Formato commenti (se necessari):
```
    No:
    if (true) //Controlla se è true

    Sì:
    // Controlla se è true.
    if (true)
```
- Evitare di usare magic numbers:
```
    No:
    var oneYearFromNow = DateTime.Now.AddMonths(12);

    Sì:
    const int MONTHS_IN_YEAR = 12;
    var oneYearFromNow = DateTime.Now.AddMonths(MONTHS_IN_YEAR);
```
- Usare `string.Empty` al posto di `""` e `Array.Empty<T>()` al posto di `new T[0]`
- Usare collezioni del tipo appropriato
- Usare collezioni di tipo `IImmutable` se non vengono modificate dopo la dichiarazione
- Usare la keyword `init` nella dichiarazione di properties readonly
```
    No:
    public string UserEmail { get; }

    Sì:
    public string UserEmail { get; init; }
```
- Usare `// TODO: Needs to be implemented` per indicare parti di codice che vanno implementate o modificate

<h3>4. Struttura delle classi</h3>

Il contenuto delle classi dovrebbe essere ordinato come segue:
- Campi `static`
- Properties `static`
- Campi `readonly`
- Campi `public`
- Properties dei servizi (es. DbContext / UnitOfWork)
- Properties pubbliche
- Costruttori
- Metodi `public` del tipo (`virtual / override`)
- Metodi `protected` del tipo (`virtual / override`)
- Metodi `public`
- Metodi `private`
- Metodi `static`
- Metodi `abstract`
- Operatori

<h3>5. Classi di Utility</h3>
- Annotarle come `static` se possibile

<div style="page-break-after: always;"></div>

# JS Code Style

Per far sì che il codice sia scritto tutto nello stesso formato, in modo che sembri scritto dalla stessa persona, e che i reviewers possano concentrarsi sul codice e non sul suo formato, adottiamo queste guidelines:

#### I nomi delle variabili, dei metodi e delle classi dovranno essere in INGLESE

<h3> 1. Convenzioni per i nomi delle classi e dei metodi</h3>
- Dare nomi sensati che rappresentino l'oggetto/classe
- Usare la notazione `camelCase` per le funzioni/metodi

<h3>2. Convenzioni per i nomi delle variabili</h3>

- Usare la notazione `camelCase` per le variabili presenti nella firme dei metodi
- Usare la notazione `camelCase` per le variabili presenti nel corpo dei metodi
- Usare la notazione `UPPER_CASE` per le costanti
- Dichiarare le variabili come `const` dove possibile

<h3>3. Leggibilità del codice</h3>

- Non allineare le graffe su una nuova linea
```
    if (true) {
        // Do something...
    } else {
        // Do something else...
    }
```
