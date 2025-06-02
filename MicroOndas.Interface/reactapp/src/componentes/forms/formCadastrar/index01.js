//\MicroOndas.Interface\reactapp\src\componentes\forms\formCadastrar\index01.js

import { useState } from "react";
import axios from 'axios';
import styles from './index01.module.css';

import Login from '../login/index01';
import Senha from '../senha/index01';
import Confirma from '../confirma/index01';
import Submit from '../submit/index01';

function Index01() {

    const [formData, setFormData] = useState({
        login: "",
        loginM:"",
        senha: "",
        senhaM: "",
        confirma: "",
        confirmaM: ""
    });
    
    let onClick = () => {        

        let valida = true;

        let vlogin = true;
        let mlogin = "";

        let vsenha = true;
        let msenha = "";

        let vconfirma = true;
        let mconfirma = "";
                
        if (formData.login.trim().length < 3 ) {
            vlogin = false;
            mlogin = "Campo obrigatório";
            valida = false;
        }

        if (formData.senha.trim().length < 4)
        {
            vsenha = false;
            msenha = "Campo obrigatório";
            valida = false;
        }
        
        if (formData.confirma.trim() !== formData.senha.trim())
        {
            vconfirma = false;
            mconfirma = "As senhas não conferem";
            valida = false;
        }

        setFormData(
            {
                ...formData,
                login: formData.login.toUpperCase().trim(),
                loginM: !vlogin ? mlogin : "",
                senhaM: !vsenha ? msenha : "",
                confirmaM: !vconfirma ? mconfirma : ""
            });

        if (valida) {

            const executa = async () => {
                try {
                    document.querySelector("#telaloading").style.display = "block";
                    let msg = document.getElementById("mensagem");

                    msg.innerHTML = "Aguarde...";
                    let resposta = await axios.post('https://localhost:44328/MicroOndas/Cadastrar', formData);
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
            <Confirma setFormData={setFormData} formData={formData} />
            <Submit onClick={onClick} Value="Cadastrar" />
        </div>
    )
}

export default Index01;
