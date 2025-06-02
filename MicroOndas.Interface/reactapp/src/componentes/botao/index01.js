// MicroOndas.Interface\reactapp\src\componentes\botao\index01.js

import propTypes from 'prop-types'
import styles from './index01.module.css'

function Index01({ Texto = "botão", OnClick = () => { }, Styles = {width:'30%',height:'10lvh'} })
{    

    return <input type="button" className={styles.index01 + " borda"} style={Styles} value={Texto} onClick={OnClick.bind(this, Texto)} />
}

Index01.propTypes =
{
    Texto: propTypes.string.isRequired,    
    OnClick: propTypes.func.isRequired,
    Styles: propTypes.object.isRequired
}

export default Index01;