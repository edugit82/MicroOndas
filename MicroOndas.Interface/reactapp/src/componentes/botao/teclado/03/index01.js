//MicroOndas.Interface\reactapp\src\componentes\botao\teclado\03\Index01.js
import Botao from '../../index01'

function Index01({OnClick }) {
    const style =
    {
        width: '30%',
        height: '10lvh',
        marginLeft: '3%',
    }

    return <Botao Texto="3" Styles={style} OnClick={OnClick} />
}

export default Index01