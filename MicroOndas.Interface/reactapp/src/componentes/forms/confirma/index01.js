//\MicroOndas.Interface\reactapp\src\componentes\forms\confirma\index01.js

import Campo from '../campo/index01';

function Index01({ setFormData, formData }) {

    function onChange(event) {
        event.preventDefault();

        setFormData({ ...formData, confirma: event.target.value });
    }

    return (
        <Campo Titulo="Confirma Senha" Tipo="password" onChange={onChange} Value={formData.confirma} Mensagem={formData.confirmaM} />
    );
}

export default Index01;
