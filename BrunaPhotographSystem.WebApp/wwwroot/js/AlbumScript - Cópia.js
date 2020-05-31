
var butaoFotos = document.querySelector(".dropdown-menu");
butaoFotos.addEventListener("show.bs.dropdown", AbrirDropZone());
butaoFotos.addEventListener("hide.bs.dropdown", FecharDropZone());

function AbrirDropZone() {
var butaoVoltar = document.querySelector("#btnVoltarLista");

butaoVoltar.style.marginTop = "15%";
}

function FecharDropZone() {
    var butaoVoltar = document.querySelector("#btnVoltarLista");

    butaoVoltar.style.marginTop = "0%";
}