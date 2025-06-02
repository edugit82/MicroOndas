//\MicroOndas\MicroOndas.Interface\reactapp\src\componentes\forms\instrucoes\index01.js

import styles from './index01.module.css';

function Index01({ setFormData, formData }) {
    function onChange(event) {
        event.preventDefault();
        setFormData({ ...formData, instrucoes: event.target.value });
    }

    return (
        <div className={styles.index01}>
            <label>Instruções:</label>
            <textarea className="borda" onChange={onChange}></textarea>
            <span>{formData.instrucoesM}</span>
        </div>        
    );
}

export default Index01;
