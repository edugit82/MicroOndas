//MicroOndas.Interface/reactapp/src/componentes/ladoesquerdo/index01.js

import styles from './index01.module.css'
import DadosPrograma from '../dadosprogramas/index01';

function Index01({mensagemProgresso})
{    

    return (
        <div className={styles.index01}>
            <div className={styles.mensagem }>
                {mensagemProgresso}
            </div>
            <div>
                <DadosPrograma/>
                <div id="corpoprojeto"></div>
            </div>
        </div>
    )
}

export default Index01;