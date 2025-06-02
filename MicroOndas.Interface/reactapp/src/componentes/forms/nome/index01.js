//\MicroOndas\MicroOndas.Interface\reactapp\src\componentes\forms\nome\index01.js

import Campo from '../campo/index01';

function Index01({ setFormData, formData }) {
    function onChange(event) {
        event.preventDefault();
        event.target.value = event.target.value.replace(/[^a-zA-Z\s]/g, '');

        setFormData({ ...formData, nome: event.target.value });
    }

    return (
        <Campo Titulo="Nome" Tipo="text" onChange={onChange} Value={formData.nome} Mensagem={formData.nomeM} />
    );
}

export default Index01;
