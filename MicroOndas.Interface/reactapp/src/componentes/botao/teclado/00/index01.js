//MicroOndas.Interface\reactapp\src\componentes\botao\teclado\00\Index01.js
import Botao from '../../index01'

function Index01({ OnClick }) {
    const style =
    {
        width: '30%',
        height: '10lvh',
        marginTop: '3%',
        marginLeft: '33%',
    }

    return <Botao Texto="0" Styles={style} OnClick={OnClick} />
}

export default Index01