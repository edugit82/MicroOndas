import './App.css';
import { BrowserRouter as Router, Switch, Route, Link } from 'react-router-dom';
import axios from 'axios';

import FormLogar from './componentes/forms/formLogar/index01';
import FormCadastrar from './componentes/forms/formCadastrar/index01';
import FormCadastrarPrograma from './componentes/forms/formCadastrarPrograma/index01';
import Tela from './componentes/tela/index01';

function App() {

    const executa = async () => {
        try {
            document.querySelector("#telaloading").style.display = "block";
            axios.get('https://localhost:44328/MicroOndas/LimpaVariaveis');
        }
        catch (error) {
            console.error("Erro ao executar:", error);
        }
        finally {
            document.querySelector("#telaloading").style.display = "none";
        }
    };
    executa();

    return (
        <div>
            <Router>
                <div>
                    <Link className="borda" to="/formLogar">Logar</Link>
                    <Link className="borda" to="/formCadastrar">Cadastrar</Link>
                    <Link className="borda" to="/formCadastrarPrograma">CadastrarPrograma</Link>
                    <Link className="borda" to="/MicroOndas">MicroOndas</Link>
                </div>
                <Switch>
                    <Route path="/formLogar">
                        <FormLogar />
                    </Route>
                    <Route path="/formCadastrar">
                        <FormCadastrar />
                    </Route>
                    <Route path="/formCadastrarPrograma">
                        <FormCadastrarPrograma />
                    </Route>
                    <Route path="/MicroOndas">
                        <Tela />
                    </Route>
                </Switch>
            </Router>
        </div>
    )
}

export default App;
