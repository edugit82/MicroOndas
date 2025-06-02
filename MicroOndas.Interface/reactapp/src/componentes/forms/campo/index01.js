//\MicroOndas.Interface\reactapp\src\componentes\forms\campo\index01.js

import PropTypes from 'prop-types';
import styles from './index01.module.css';

function Index01({ Titulo = "Campo", Tipo = "text", onChange = () => { }, Value = "", Mensagem = "" })
{
    return (
        <div className={styles.index01}>
            <label>{Titulo + ":"}</label>
            <input type={Tipo} className="borda" onChange={onChange} value={Value} />
            <span>{Mensagem}</span>
        </div>
    )
}

Index01.propTypes =
{
    Titulo: PropTypes.string,
    Tipo: PropTypes.string,
    onChange: PropTypes.func,
    Value: PropTypes.string,
    Mensagem: PropTypes.string
}

export default Index01;