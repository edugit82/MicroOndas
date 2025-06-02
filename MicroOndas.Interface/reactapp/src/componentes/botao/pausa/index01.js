//\MicroOndas.Interface\reactapp\src\componentes\botao\pausa\index01.js

import Botao from '../index01'
import axios from 'axios';

function Index01() {
    const style =
    {
        width: '30%',
        height: '10lvh',
        marginLeft: '4.5%',
    }

    let onClick = async (param, event) =>
    {
        try
        {
            document.querySelector("#telaloading").style.display = "block";
            await axios.get('https://localhost:44328/MicroOndas/Pausa');
            
        } catch (error) {
            console.error('Error:', error.response?.data || error.message);
        }
        finally {
            document.querySelector("#telaloading").style.display = "none";
        }
    }

    return <Botao Texto="Pausa" Styles={style} OnClick={onClick} />
}

export default Index01