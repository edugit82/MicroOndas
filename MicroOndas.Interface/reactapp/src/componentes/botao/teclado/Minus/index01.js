//MicroOndas.Interface\reactapp\src\componentes\botao\teclado\Minus\Index01.js
import Botao from '../../index01'

function Index01({ OnClick }) {
    const style =
    {
        width: '40%',
        height: '10lvh'
    }

    return <Botao Texto="-" Styles={style} OnClick={OnClick} />
}

export default Index01