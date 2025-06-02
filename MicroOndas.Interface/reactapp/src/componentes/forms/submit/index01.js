//\MicroOndas.Interface\reactapp\src\componentes\forms\submit\index01.js

import styles from './index01.module.css';

function Index01({ onClick = () => { }, Value = "Enviar"})
{
    return <button className={styles.index01} onClick={onClick}>{Value}</button>
}

export default Index01;