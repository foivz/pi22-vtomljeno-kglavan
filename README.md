# Naziv projekta
INVENTura

## Projektni tim

Ime i prezime   | E-mail adresa (FOI) | JMBAG      | Github korisničko ime
--------------  | ------------------- | ---------- | ---------------------
Vid Tomljenović | vtomljeno@foi.hr    | 0016137275 | VidTomljenovic
Kristina Glavan | kglavan@foi.hr      | 0016123743 | Kristina-Glavan


## Opis domene
Projekt pokriva problem provođenja inventure odnosno brojanja zaliha na tzv. "daily", "weekly", "monthly" bazama na način ažuriranja stanja zaliha svaki puta kada kupac naručuje proizvod. 

## Specifikacija projekta
Mogućnost stvaranja novog korisnika te prijava kao kupac ili administrator. Kupac ima mogućnost naručivanja odjevnog artikla kroz aplikaciju. Zalihe bi se u bazi ažurirale, pa bi sustav slao povratnu informaciju o stanju artikala (vidljivo naravno samo administratoru) u stvarnome vremenu. Ovim putem bi se smanjile potrebe za "ručnim brojanjem" te olakšalo naručivanje potrebnih arrtikala za budućnost od strane administratora. CRUD rad s bazom je pokriven onda na način da kupac R(ead) koje artikle može naručiti, dok administrator može C(reate) nove, D(elete) stare i U(pdate) zalihe kako bi zadržao prodaju i kompetentnost na tržištu.   


Oznaka | Naziv | Kratki opis        | Odgovorni član tima
------ | ----- | ------------------ | -------------------
F01    | stvaranje korisnika prijava korisnika       | otvaranjem aplikacije dobiva se izbor stvaranja novog profila ili prijave u postojeći, odabirom prijave u postojeći račun dobiva se forma "Login-a"  | Vid Tomljenović                 
F02    | izbor i pregled proizvoda  | korisnik odnosno kupac dobiva uvid u različite odjevne artikle  koji su naravno različitih veličina, cijena | Vid Tomljenović
F03    | naručivanje artikala       | kupac može naručiti odabrani artikl, a za isti mu se generira generic račun | Vid Tomljenović
F04    | uvid u stanje zaliha i naručivanje artikala | s administratorove strane moguć je uvid u stanje zaliha, kojima se raspolaže za slaganje konačnih proizvoda, moguća je narudžba artikala kako bi se popunile zalihe                                                                       | Kristina Glavan
F05    | stvaranje / brisanje artikala | s administratorove strane moguće je ažurirati artikle                               | Kristina Glavan
F06    | komentari | kupac može ostaviti komentar na artikl za druge kupce kako bi mogli isti pročitati i dobiti više stvarnih informacija | Kristina Glavan

## Tehnologije i oprema
MySQL, MS Visual Studio, Figma
