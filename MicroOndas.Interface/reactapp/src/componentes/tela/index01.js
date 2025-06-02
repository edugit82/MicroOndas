//\MicroOndas\MicroOndas.Interface\reactapp\src\componentes\tela\index01.js

import { useState, useEffect } from "react";
import axios from 'axios';
import styles from './index01.module.css';

import LadoDireito from '../ladodireito/index01';
import LadoEsquerdo from '../ladoesquerdo/index01';

function Index01()
{
    const [mensagemDisplay, setMensagemDisplay] = useState("");
    const [mensagemProgresso, setMensagemProgresso] = useState("");
    
    useEffect(() => {

        //Timer atualização de progresso
        let dalayTimer = async () => {
            do {                

                try {

                    await axios.post('https://localhost:44328/MicroOndas/Progresso');

                    let displayresponse = await axios.get('https://localhost:44328/MicroOndas/MensagemDisplay');
                    setMensagemDisplay(displayresponse.data.retorno);

                    let progressoresponse = await axios.get('https://localhost:44328/MicroOndas/MensagemProgresso');
                    setMensagemProgresso(progressoresponse.data.retorno);

                } catch (error) {
                    console.error('Error:', error.response?.data || error.message);
                }

                await new Promise(resolve => setTimeout(resolve, 1000));
            }
            while (true);
        };
        dalayTimer();

    }, [setMensagemDisplay, setMensagemProgresso]);

    return (
        <div className={styles.index01 }>
            <LadoEsquerdo mensagemProgresso={mensagemProgresso}/>
            <LadoDireito mensagemDisplay={mensagemDisplay} />
        </div>
    )    
}

export default Index01;