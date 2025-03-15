# ShoesShelf

Task from the employer:

Design a relational database model for an application that manages a large shoe inventory in a bowling alley.

**The data model must include:**

- Several database tables and relationships between them
- Attributes with appropriate data types
- Foreign and primary keys
- Unique indexes, if necessary

**The application must allow the following:**

Record shoe types and specific pairs of shoes of each type, including their sizes, date added to the inventory, purchase price, total number of rentals for each pair, date of last disinfection, type and severity of damage, and whether the shoes are currently rented out or not.
The application should provide input for purchasing new shoes as replacements for damaged ones (how many shoes, which sizes, and types).
The application will evaluate which shoe types are the most popular and suggest purchasing those, considering the purchase price as well.

# ShoesShelf - Project Details

Analytical solution design: A relational database model for managing a shoe inventory in a bowling alley. The application has several tables with foreign and primary keys and is populated with data. The SHOES tab demonstrates data filtering. The tabs support sorting and pagination. The application is designed in English. Analytical capabilities of the program are available in the Reports tab:

• Rental Report: The application shows the number of rentals for each shoe type and size, sorted by popularity (from the highest number of rentals, then by price).
• Disinfection Report: The application displays each pair of shoes with the date of its last disinfection. If a new pair is added without disinfection, the report will show "Not yet disinfected" for that pair.
• Substitution Report: The application suggests purchasing shoes with critical damage levels. For example, if there are 3 pairs of men's shoes of the same brand and size with critical damage, the application will suggest purchasing 3 new pairs of those shoes.

The detail of each shoe pair lists all its attributes, including type, size, date added to the inventory, purchase price, total number of rentals, last disinfection date, damage types and severity, and whether the shoes are currently rented out.

The application includes an authentication system. **Adding, modifying, and deleting records is allowed for the administrator account (pre-configured).**

The HTML design is interactive and uses Bootstrap libraries.


# ShoesShelf

Zadání od zaměstnavatele:

Navrhněte relační databázový model pro aplikaci pro správu velkého botníku v bowlingové herně

**Datový model musí obsahovat:**

- několik databázových tabulek a vazby mezi nimi
- atributy včetně vhodných datových typů
- cizí a vlastní klíče
- primární klíče, případné unikátní indexy budou-li třeba

**Aplikace musí umožňovat zjistit:**

Evidence typů bot i konkrétních párů bot určitého typu, jejich velikostí, datum zařazení do botníku, pořizovací cena, celkový počet vypůjčení pro daný pár, termín poslední dezinfekce, druh a závažnost poškození a zda je daný pár bot aktuálně zapůjčený nebo ne. Aplikace bude poskytovat podklad pro nákup nových bot jako náhrady za poškozené (kolik bot, jaké velikosti a typy). Aplikace bude vyhodnocovat, které typy bot jsou nejoblíbenější a navrhovat k zakoupení především takové typy, ovšem parametricky i s ohledem na pořizovací cenu.

# ShoesShelf - projekt dle zadání

Analytický návrh řešení: relační databázový model pro správu botníku v bowlingové herně. Aplikace má několik tabulek s cizími i vlastními klíči, je naplněna daty. Na záložce SHOES je předvedeno filtrování dat. Záložky mají řazení a stránkování. Aplikace je navržena v angličtině. Analytické schopnosti programu jsou na záložce Reports:

• Rental Report: aplikace udává množství vypůjčení bot stejného typu a velikosti, řazeno podle oblíbenosti, tedy od největšího počtu vypůjčení a pak podle ceny;
• Disinfection Report: aplikace vypisuje každý pár bot s uvedením data jen poslední desinfekce. V případě zařazení nového páru bot bez dezinfekce, se v Reportu u tohoto páru vyskytne záznam "Not yet disinfected";
• Substitution Report: aplikace navrhuje nákup bot s kritickou mírou poškození. V případě, že jsou v botníku 3 páry např. pánských bot stejné značky a velikosti s kritickým poškozením, navrhne aplikace k zakoupení 3 takové páry.

Detail jednotlivého páru bot vypíše veškeré jeho vlastnosti včetně typu, velikosti, data zařazení do botníku, pořizovací ceny, celkového počtu vypůjčení pro daný pár, termínu poslední dezinfekce, druhů a závažnosti poškození, a zda je daný pár bot aktuálně zapůjčený.

V aplikaci je implementován autentizační systém. **Přidání, úprava a odstranění záznamů je umožněno účtu administrátora (přednastaveno).**

HTML úprava je navržena s použitím knihoven Bootstrap a je interaktivní.
