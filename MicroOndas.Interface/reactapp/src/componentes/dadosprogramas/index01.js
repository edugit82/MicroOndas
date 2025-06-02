//\MicroOndas\MicroOndas.Interface\reactapp\src\componentes\dadosprogramas\index01.js
import { useState, useEffect } from 'react';
import axios from 'axios';
import styles from './index01.module.css';

function Index()
{
    const [dados, setDados] = useState([]);

    useEffect(() =>
    {
        const coordenadas = (obj) => {

            let _top = obj.offsetTop;
            let _left = obj.offsetLeft;
            let _width = obj.offsetWidth;
            let _height = obj.offsetHeight;
            let _bottom = _top + _height;
            let _right = _left + _width;

            let dados = {
                top: _top,
                left: _left,
                width: _width,
                height: _height,
                bottom: _bottom,
                right: _right
            };

            return dados;
        };

        const onmouseenter = async (e) =>
        {
            let content = "";

            try
            {                
                const response = await axios.post('https://localhost:44328/MicroOndas/DescricaoPrograma', { id: e.target.id });
                content = response.data.retorno;

            } catch (error) {
                console.error("Erro ao obter programas:", error);                
            }            

            const coord = coordenadas(e.target);            

            const top = (coord.bottom + 150);
            const left = (coord.right + 30);            
            
            // Step 3: Append the new div to the desired parent element            
            document.querySelector("#corpoprojeto").innerHTML = content;
            document.querySelector("#corpoprojeto").style.cssText = "position: fixed;top:" + top + "px;left:" + left + "px;background-color: white;color: black;width: 30vw;z-index: 1000;padding:1%;";            
            
        };
        const onmouseleave = (e) =>
        {            
            let child = document.getElementById("corpoprojeto");
            
            if (child) {
                child.style.cssText = "display:none;"; // Removes the element directly
            }
        };

        const onclick = async (e) =>
        {
            try {
                document.querySelector("#telaloading").style.display = "block";
                const response = await axios.post('https://localhost:44328/MicroOndas/AquecimentoPost', { id: e.target.id, tempo: "00:00", potencia: "0" });
                console.log(response.data.retorno);                

            } catch (error) {
                console.error("Erro ao selecionar programa:", error);
            }
            finally {
                document.querySelector("#telaloading").style.display = "none";
            }
        };

        const fetchData = async () => {
            try {
                document.querySelector("#telaloading").style.display = "block";
                const response = await axios.get('https://localhost:44328/MicroOndas/GetTituloProgramas');
                                
                let lista = response.data.retorno.map((a, index) =>
                {
                    return (<button className={styles.programa + " borda"} onMouseEnter={onmouseenter} onMouseLeave={onmouseleave} onClick={onclick} id={a.index} key={a.index} >{a.nome}</button>)
                });

                return lista;
                
            } catch (error) {
                console.error("Erro ao obter programas:", error);
                return [];
            }
            finally {
                document.querySelector("#telaloading").style.display = "none";
            }
        };

        setDados(fetchData());

    }, [setDados]);

    return (<>{dados}</>)
}

export default Index;