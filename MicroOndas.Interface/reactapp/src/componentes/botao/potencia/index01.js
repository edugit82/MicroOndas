// MicroOndas.Interface\reactapp\src\componentes\botao\potencia\index01.js

import Botao from '../index01'

function Index01({ SetMostraTimer, SetMostraPotencia }) {
    const style =
    {
        width: '30%',
        height: '10lvh',
        marginLeft: '12.5%'
    }

    function onClick(param, event)
    {
        SetMostraTimer(false);
        SetMostraPotencia(true);
    }

    return <Botao Texto="Potência" Styles={style} OnClick={onClick} />
}

export default Index01