document.getElementById("adicionarCarro").addEventListener("click", () => {
    const id = parseInt(document.getElementById("inputId").value);
    const modelo = document.getElementById("inputModelo").value;
    const marca = document.getElementById("inputMarca").value;
    const ano = parseInt(document.getElementById("inputAno").value);
    const placa = document.getElementById("inputPlaca").value;

    fetch("/carros", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify({ id, modelo, marca, ano, placa })
    })
    .then(response => {
        if (response.ok) {
            location.reload();
        } else {
            alert("Erro ao adicionar carro");
        }
    });
});
