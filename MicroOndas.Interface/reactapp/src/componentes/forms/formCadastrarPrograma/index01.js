//\MicroOndas.Interface\reactapp\src\componentes\forms\formCadastrarPrograma\index01.js

import { useState } from "react";
import axios from 'axios';
import styles from './index01.module.css';

import Nome from '../nome/index01';
import Alimento from '../alimento/index01';
import Tempo from '../tempo/index01';
import Potencia from '../potencia/index01';
import Progresso from '../progresso/index01';
import Instrucoes from '../instrucoes/index01';
import Submit from '../submit/index01';

function Index01() {

    const [formData, setFormData] = useState({
        nome: "",
        nomeM: "",
        alimento: "",
        alimentoM: "",
        tempo: "",
        tempoM: "",
        potencia: "",
        potenciaM: "",
        progresso: "",
        progressoM: "",
        instrucoes: "",
        instrucoesM: "",
    });

    function onClick() {

        let valida = true;

        let vnome = true;
        let mnome = "";

        let valimento = true;
        let malimento = "";

        let vtempo = true;
        let mtempo = "";

        let vpotencia = true;
        let mpotencia = "";

        let vprogresso = true;
        let mprogresso = "";

        let vinstrucoes = true;
        let minstrucoes = "";

        if (formData.nome.trim().length < 3) {
            vnome = false;
            mnome = "Campo obrigatório";

            valida = false;
        }

        if (formData.alimento.trim().length < 3) {
            valimento = false;
            malimento = "Campo obrigatório";

            valida = false;
        }

        if (formData.tempo.trim() === "00:00" || formData.tempo.trim() === "0000" || formData.tempo.trim() === "") {
            vtempo = false;
            mtempo = "Campo obrigatório";

            valida = false;
        }

        if (formData.potencia.trim() === "") {
            vpotencia = false;
            mpotencia = "Campo obrigatório";

            valida = false;
        }

        if (formData.progresso.trim() === "") {
            vprogresso = false;
            mprogresso = "Campo obrigatório";

            valida = false;
        }

        if (formData.instrucoes.trim() === "") {
            vinstrucoes = false;
            minstrucoes = "Campo obrigatório";

            valida = false;
        }

        setFormData(
            {
                ...formData,
                nome: formData.nome.toUpperCase().trim(),
                nomeM: !vnome ? mnome : "",
                alimento: formData.alimento.toUpperCase().trim(),
                alimentoM: !valimento ? malimento : "",                
                tempoM: !vtempo ? mtempo : "",
                potenciaM: !vpotencia ? mpotencia : "",
                progressoM: !vprogresso ? mprogresso : "",
                instrucoesM: !vinstrucoes ? minstrucoes : "",
            });

        if (valida)
        {
            const executa = async () => {
                try {
                    document.querySelector("#telaloading").style.display = "block";
                    let msg = document.getElementById("mensagem");

                    msg.innerHTML = "Aguarde...";
                    let resposta = await axios.post('https://localhost:44328/MicroOndas/CadastrarPrograma', formData);
                    msg.innerHTML = resposta.data.retorno;
                }
                catch (error) {
                    console.error("Erro ao executar:", error);
                }
                finally {
                    document.querySelector("#telaloading").style.display = "none";
                }
            };
            executa();
        }
    }

    return (
        <div className={styles.index01}>
            <label id="mensagem" className={styles.mensagem}></label>
            <Nome setFormData={setFormData} formData={formData} />
            <Alimento setFormData={setFormData} formData={formData} />
            <Tempo setFormData={setFormData} formData={formData} />
            <Potencia setFormData={setFormData} formData={formData} />
            <Progresso setFormData={setFormData} formData={formData} />
            <Instrucoes setFormData={setFormData} formData={formData} />
            <Submit onClick={onClick} />
        </div>
    )
}

export default Index01;
