//\MicroOndas\MicroOndas.Interface\reactapp\src\componentes\forms\alimento\index01.js

import Campo from '../campo/index01';

function Index01({ setFormData, formData }) {
    function onChange(event) {
        event.preventDefault();
        event.target.value = event.target.value.replace(/[^a-zA-Z\s]/g, '');

        setFormData({ ...formData, alimento: event.target.value });
    }

    return (
        <Campo Titulo="Alimento" Tipo="text" onChange={onChange} Value={formData.alimento} Mensagem={formData.alimentoM} />
    );
}

export default Index01;
