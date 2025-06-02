//\MicroOndas\MicroOndas.Interface\reactapp\src\componentes\forms\tempo\index01.js

import Campo from '../campo/index01';

function Index01({ setFormData, formData }) {
    function onChange(event) {
        event.preventDefault();

        // Limpa caracteres não numéricos, preenche com zeros à esquerda, limita a 4 caracteres e formata como MM:SS
        event.target.value = event.target.value.replace(/[^0-9]/g, '');

        if (event.target.value.length > 4)
        event.target.value = event.target.value.slice(event.target.value.length - 4);

        event.target.value = event.target.value.padStart(4, '0'); // Preenche com zeros à esquerda
        event.target.value = event.target.value.slice(0, 4); // Limita a 4 caracteres
        event.target.value = event.target.value.replace(/(\d{2})(\d{2})/, '$1:$2'); // Formata como MM:SS        

        setFormData({ ...formData, tempo: event.target.value });
    }

    return (
        <Campo Titulo="Tempo (00:00)" Tipo="text" onChange={onChange} Value={formData.tempo} Mensagem={formData.tempoM} />
    );
}

export default Index01;
