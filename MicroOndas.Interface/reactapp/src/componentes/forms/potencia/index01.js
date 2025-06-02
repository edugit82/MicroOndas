//\MicroOndas\MicroOndas.Interface\reactapp\src\componentes\forms\potencia\index01.js

import Campo from '../campo/index01';

function Index01({ setFormData, formData }) {
    function onChange(event) {
        event.preventDefault();

        // Limpa caracteres não numéricos, preenche com zeros à esquerda, limita a 4 caracteres e formata como MM:SS
        event.target.value = event.target.value.replace(/[^0-9]/g, '');

        if (event.target.value.length > 0)
        {
            let num = parseInt(event.target.value);
            num = num < 1 || num > 10 ? 10 : num; // Limita a potência entre 1 e 10
            event.target.value = num.toString();
        }

        setFormData({ ...formData, potencia: event.target.value });
    }

    return (
        <Campo Titulo="Potencia (1-10)" Tipo="text" onChange={onChange} Value={formData.potencia} Mensagem={formData.potenciaM} />
    );
}

export default Index01;
