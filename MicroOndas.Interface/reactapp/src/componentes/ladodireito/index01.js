// MicroOndas.Interface/reactapp/src/componentes/ladodireito/index01.js

import { useState} from "react";
import axios from 'axios';

import styles from './index01.module.css'

import Display from '../display/index01'
import Timer from '../botao/timer/index01'
import Potencia from '../botao/potencia/index01'

import Botao01 from '../botao/teclado/01/index01'
import Botao02 from '../botao/teclado/02/index01'
import Botao03 from '../botao/teclado/03/index01'
import Botao04 from '../botao/teclado/04/index01'
import Botao05 from '../botao/teclado/05/index01'
import Botao06 from '../botao/teclado/06/index01'
import Botao07 from '../botao/teclado/07/index01'
import Botao08 from '../botao/teclado/08/index01'
import Botao09 from '../botao/teclado/09/index01'
import Botao00 from '../botao/teclado/00/index01'

import Plus from '../botao/teclado/Plus/index01'
import Minus from '../botao/teclado/Minus/index01'

import Inicio from '../botao/inicio/index01'
import InicioRapido from '../botao/iniciorapido/index01'
import Pausa from '../botao/pausa/index01'

function Index01({ mensagemDisplay })
{     
    const [MostraTimer, setMostraTimer] = useState(false);
    const [MostraPotencia, setMostraPotencia] = useState(false);        

    const TimerClick = async (param, event) => {
        const viewmodel = {
            tipo: 1,
            texto: param,
        };

        try {
            await axios.post('https://localhost:44328/MicroOndas/TextoBotao', viewmodel);
        }
        catch (error) {
            console.error('Error:', error.response?.data || error.message);
        }
    };

    const PotenciaClick = async (param, event) => {

        const viewmodel = {
            tipo: 2,
            texto: param,
        };

        try {
            document.querySelector("#telaloading").style.display = "block";
            await axios.post('https://localhost:44328/MicroOndas/TextoBotao', viewmodel);
        }
        catch (error) {
            console.error('Error:', error.response?.data || error.message);
        }
        finally
        {
            document.querySelector("#telaloading").style.display = "none";
        }
    };
    

    return (
        <div className={styles.index01}>
            <Display Texto={mensagemDisplay} />
            <div className={styles.botoes}>
                <Timer SetMostraTimer={setMostraTimer} SetMostraPotencia={setMostraPotencia} />
                <Potencia SetMostraTimer={setMostraTimer} SetMostraPotencia={setMostraPotencia} />
            </div>
            <div className={styles.interatividade}>
                <div className={styles.teclado} style={{ display: MostraTimer ? "block" : "none" }}>
                    <Botao01 OnClick={TimerClick} />
                    <Botao02 OnClick={TimerClick} />
                    <Botao03 OnClick={TimerClick} />
                    <Botao04 OnClick={TimerClick} />
                    <Botao05 OnClick={TimerClick} />
                    <Botao06 OnClick={TimerClick} />
                    <Botao07 OnClick={TimerClick} />
                    <Botao08 OnClick={TimerClick} />
                    <Botao09 OnClick={TimerClick} />
                    <Botao00 OnClick={TimerClick} />
                </div>
                <div className={styles.tecladopotencia} style={{ display: MostraPotencia ? "block" : "none" }}>
                    <Minus OnClick={PotenciaClick} />
                    <Plus OnClick={PotenciaClick} />
                </div>
            </div>
            <div className={styles.acao}>
                <Inicio />
                <InicioRapido />
                <Pausa />
            </div>            
        </div>         
    )
}

export default Index01;