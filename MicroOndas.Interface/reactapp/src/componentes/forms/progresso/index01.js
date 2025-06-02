//\MicroOndas\MicroOndas.Interface\reactapp\src\componentes\forms\progresso\index01.js

import Campo from '../campo/index01';

function Index01({ setFormData, formData }) {
    function onChange(event) {
        event.preventDefault();

        // só caracteres especiais
        event.target.value = event.target.value.replace(/[0-9a-zA-z\s]/g, '');

        if (event.target.value.length > 0) {
            event.target.value = event.target.value.slice(event.target.value.length-1); // Limita a 4 caracteres
        }

        setFormData({ ...formData, progresso: event.target.value });
    }

    return (
        <Campo Titulo="Progresso: ($,#,%,etc.)" Tipo="text" onChange={onChange} Value={formData.progresso} Mensagem={formData.progressoM} />
    );
}

export default Index01;
