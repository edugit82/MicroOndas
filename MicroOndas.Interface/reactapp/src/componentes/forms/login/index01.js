//\MicroOndas\MicroOndas.Interface\reactapp\src\componentes\forms\login\index01.js

import Campo from '../campo/index01';

function Index01({setFormData, formData })
{    
    function onChange(event)
    {                
        event.preventDefault();        
        event.target.value = event.target.value.replace(/[^a-zA-Z\s]/g, '');

        setFormData({ ...formData, login: event.target.value });
    }    

    return (
        <Campo Titulo="Login" Tipo="text" onChange={onChange} Value={formData.login} Mensagem={formData.loginM} />
    );
}

export default Index01;
