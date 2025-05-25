async function carregarCarros() {
  const resposta = await fetch('/carros');
  const carros = await resposta.json();
  const tabela = document.querySelector("#carros-table tbody");
  tabela.innerHTML = '';

  carros.forEach(carro => {
    const linha = document.createElement('tr');
    linha.innerHTML = `
      <td>${carro.id}</td>
      <td>${carro.modelo}</td>
      <td>${carro.marca}</td>
      <td>${carro.ano}</td>
      <td>${carro.placa}</td>
      <td><button class="delete" onclick="deletarCarro(${carro.id})">Excluir</button></td>
    `;
    tabela.appendChild(linha);
  });
}

async function adicionarCarro() {
  const idInput = document.getElementById("id").value.trim();
  const modeloInput = document.getElementById("modelo").value.trim();
  const marcaInput = document.getElementById("marca").value.trim();
  const anoInput = document.getElementById("ano").value.trim();
  const placaInput = document.getElementById("placa").value.trim();

  // Validação simples
  if (!idInput || !modeloInput || !marcaInput || !anoInput || !placaInput) {
    alert("Por favor, preencha todos os campos antes de adicionar.");
    return;
  }

  // Converte valores numéricos
  const carro = {
    id: parseInt(idInput),
    modelo: modeloInput,
    marca: marcaInput,
    ano: parseInt(anoInput),
    placa: placaInput
  };

  console.log("Tentando adicionar carro:", carro);

  await fetch('/carros', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify(carro)
  });

  carregarCarros();

  // Limpa os campos após adicionar
  document.getElementById("id").value = '';
  document.getElementById("modelo").value = '';
  document.getElementById("marca").value = '';
  document.getElementById("ano").value = '';
  document.getElementById("placa").value = '';
}

async function deletarCarro(id) {
  await fetch(`/carros/${id}`, { method: 'DELETE' });
  carregarCarros();
}

// Carrega a lista ao abrir a página
carregarCarros();
