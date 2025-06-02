// MicroOndas.Interface\reactapp\src\componentes\display\index01.js

import styles from './index01.module.css'

function Index({Texto})
{
    return (
        <div className={styles.index01 + " borda"}>
            {Texto}
        </div>
    )
}

export default Index
