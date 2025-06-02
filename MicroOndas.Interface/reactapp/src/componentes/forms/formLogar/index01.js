//\MicroOndas.Interface\reactapp\src\componentes\forms\formLogar\index01.js

import { useState } from "react";
import axios from 'axios';
import styles from './index01.module.css';

import Login from '../login/index01';
import Senha from '../senha/index01';
import Submit from '../submit/index01';

function Index01() {

    const [formData, setFormData] = useState({
        logar: "",
        logarM: "",
        senha: "",
        senhaM: ""
    });

    function onClick() {

        let valida = true;

        let vlogar = true;
        let mlogar = "";

        let vsenha = true;
        let msenha = "";

        if (formData.login.trim().length < 3) {
            vlogar = false;
            mlogar = "Campo obrigatório";

            valida = false;
        }

        if (formData.senha.trim().length < 4) {
            vsenha = false;
            msenha = "Campo obrigatório";

            valida = false;
        }        

        setFormData(
            {
                ...formData,
                login: formData.login.toUpperCase().trim(),
                loginM: !vlogar ? mlogar : "",
                senhaM: !vsenha ? msenha : ""
            });

        if (valida)
        {
            const executa = async () =>
            {
                try
                {
                    document.querySelector("#telaloading").style.display = "block";
                    let msg = document.getElementById("mensagem");

                    msg.innerHTML = "Aguarde...";
                    let resposta = await axios.post('https://localhost:44328/MicroOndas/Logar', formData);
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
            <Login setFormData={setFormData} formData={formData} />
            <Senha setFormData={setFormData} formData={formData} />            
            <Submit onClick={onClick} Value="Logar" />
        </div>        
    )
}

export default Index01;
