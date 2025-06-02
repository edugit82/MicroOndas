//MicroOndas.Interface\reactapp\src\componentes\botao\teclado\08\Index01.js
import Botao from '../../index01'

function Index01({OnClick }) {
    const style =
    {
        width: '30%',
        height: '10lvh',
        marginTop: '3%',
        marginLeft: '3%',
    }

    return <Botao Texto="8" Styles={style} OnClick={OnClick} />
}

export default Index01