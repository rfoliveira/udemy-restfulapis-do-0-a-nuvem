import React from 'react';

export default function Header({children}) {
    return (
        <header>
            <h1>{children}</h1>
        </header>
    )
}
/*
Usando properties

export default function Header(props) {
    return (
        <header>
            <h1>{props.title}</h1>
        </header>
    )
}

Exemplo no chamador:

export default function App() {
  return (
    <Header title="Client REST Udemy - properties"></Header>
  );
}
*/