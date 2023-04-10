/* variabili per impostazioni del menu */

// altezza voci menu
altezzaVoci = 18;

// di quanti pixel il menu si muoverà ogni volta
muoviVoce = 10;

// Velocità di movimento del menu
velocitaMenu = 40;

// usata per controllare il tempo
var tempo;

// funzione utilizzata per creare il menu
// definisce un nuovo oggetto denominato CreaMenu 
function CreaMenu(obj, nest) {   // obj = 'divMenu' , cioè una stringa dal nome 'divMenu'
    nest = (!nest) ? "" : 'document.' + nest + '.';
    
    this.css = eval(obj + '.style');    //CreaMenu.css = valuta l'espressione 'divMenu.style' - che ritorna un  oggetto che rappresenta un CSS declaration block, and exposes style information and various style-related methods and properties, cioè tutti le possibili priorità di stile di un oggetto generico HTML (esmepio un DIV)
    //this.top = DaTop(this.css);
    this.state = 1;                     //CreaMenu.state = 1
    this.go = 0;                        //CreaMenu.go = 0
    this.height = eval(obj + '.offsetHeight'); //CreaMenu.height = valuta l'espressione 'divMenu.offsetHeight' che recupera l'altezza di "divMenu"
    
    this.obj = obj + "Object";  // Creamenu.obj = crea la stringa "divMenuObject"
    eval(this.obj + "=this");   // valuta divMenuObject=this e siccome this è un oggetto di tipo CreaMenu (che avrà tutte le proprietà impostate come abbiamo visto primo con this) divMenuObjct diventa un oggetto di tipo CreaMenu
}

// questa funzione restituisce la distanza dell'oggetto dal punto più alto
function DaTop(obj) {
    var gleft = parseInt(obj.top, 10);
    return gleft;
}

/* tramite questa funzione viene resa
 * la senzazione di "movimento" del menu
 * quando si clicca su unio degli elementi
 */  
function muoviMenu() {
    clearTimeout(tempo);
    !oMenu.state ? NascondiMenu() : VisualizzaMenu();
}

/* tramite questa funzione il menu viene nascosto,
 * spostandolo di tanti passi per volta quanti contrassegnati su muoviVoce
 * ed alla velocità indicata da velocitaMenu
 */
function NascondiMenu() {
    if (oMenu.top() > eval(scrolled) - oMenu.height + altezzaVoci) {
        oMenu.go = 1;
        oMenu.css.top = oMenu.top() - muoviVoce;
        tempo = setTimeout("NascondiMenu()", velocitaMenu);
    }
    else {
        oMenu.go = 0;
        oMenu.state = 1;
    }
}

/* tramite questa funzione il menu viene visualizzato,
 * spostandolo di tanti passi  quanti contrassegnati su muoviVoce
 * ed alla velocità indicata da velocitaMenu
 */
function VisualizzaMenu() {
    if (oMenu.css.top() < eval(scrolled)) {
        oMenu.go = 1;
        oMenu.css.top = oMenu.top() + muoviVoce;
        tempo = setTimeout("VisualizzaMenu()", velocitaMenu);
    }
    else {
        oMenu.go = 0;
        oMenu.state = 0;
    }
}

/* funzione che controlla se il menu è visibile o meno */
function controllaStato() {
    if (!oMenu.go) {
        if (!oMenu.state)
            oMenu.state = scrolled;
        else
            oMenu.state = eval(scrolled) - oMenu.height + altezzaVoci;
    }
}

/* funzione utilizzata per inizializzare il menu e le sue proprieta */
function InizializzaMenu() {
    oMenu = new CreaMenu('divMenu');
    scrolled = "document.body.scrollTop";
    oMenu.css.top = eval(scrolled) - oMenu.height + altezzaVoci;
    oMenu.css.visibility = "visible";
    window.onscroll = controllaStato;

}

//* carica la finestra
window.onload = InizializzaMenu;
