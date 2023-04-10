function avvisa() {
    alert("Un esempio di finestra di avviso");
}

// costruttore dell'oggetto tringolo
// con "this" si valorizzano le proprietà dell'oggetto
function Triangolo(a, b, c) {
    this.perimetro = a + b + c;
}