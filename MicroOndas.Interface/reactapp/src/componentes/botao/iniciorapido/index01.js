//\MicroOndas.Interface\reactapp\src\componentes\botao\iniciorapido\index01.js

import Botao from '../index01'
import axios from 'axios';

function Index01() {
    const style =
    {
        width: '30%',
        height: '10lvh',
        marginLeft: '4.5%',
    }

    let onClick = async (param, event) => {

        let viewmodel = {
            tipo: 1,
            texto: "00:30",
        };

        try {
            document.querySelector("#telaloading").style.display = "block";
            await axios.post('https://localhost:44328/MicroOndas/TextoBotao', viewmodel);
            await axios.get('https://localhost:44328/MicroOndas/Aquecimento');
            
        } catch (error) {
            console.error('Error:', error.response?.data || error.message);
        }
        finally {
            document.querySelector("#telaloading").style.display = "none";
        }
    }

    return <Botao Texto="Inicio Rápido" Styles={style} OnClick={onClick} />
}

export default Index01