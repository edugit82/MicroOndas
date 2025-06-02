//MicroOndas.Interface\reactapp\src\componentes\botao\teclado\01\Index01.js
import Botao from '../../index01'

function Index01({OnClick})
{
    const style =
    {
        width: '30%',
        height: '10lvh'        
    }    

    return <Botao Texto="1" Styles={style} OnClick={OnClick} />
}

export default Index01