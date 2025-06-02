//\MicroOndas.Interface\reactapp\src\componentes\forms\senha\index01.js

import Campo from '../campo/index01';

function Index01({ setFormData, formData }) {

    function onChange(event) {
        event.preventDefault();
        
        setFormData({ ...formData, senha: event.target.value });
    }

    return (
        <Campo Titulo="Senha" Tipo="password" onChange={onChange} Value={formData.senha} Mensagem={formData.senhaM} />
    );
}

export default Index01;
