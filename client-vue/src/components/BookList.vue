<template>
    <div className="book-container">
        <header>
            <img src=../assets/logo.svg alt="REST API Logo" />
            <span>Welcome <strong>{{username}}</strong>!</span>
            <Link className="button" to="book/new">Add new book</Link>
            
            <!-- ref.: ícone obtido em https://feathericons.com/ 
                Para usar precisa instalar o "react-icons" -> npm install react-icons 

                Ver como user para o vuejs...
            -->
            <button type="button">
                <!-- <FiPower size={18} color="#251fc5" /> -->
            </button>
        </header>

        <h1>Registered books</h1>
        <ul>
            <li>
                <strong>Title:</strong>
                <p>Docker Deep Dive</p>
                <strong>Author:</strong>
                <p>Nigel Poulton</p>
                <strong>Price:</strong>
                <p>R$ 47,90</p>
                <strong>Release date:</strong>
                <p>12/07/2017</p>

                <button type="button">Edit
                    <!-- <FiEdit size={20} color="#251fc5" /> -->
                </button>    
                <button type="button">Remove
                    <!-- <FiTrash2 size={20} color="#251fc5" /> -->
                </button>    
            </li> 
            <li>
                <strong>Title:</strong>
                <p>Docker Deep Dive</p>
                <strong>Author:</strong>
                <p>Nigel Poulton</p>
                <strong>Price:</strong>
                <p>R$ 47,90</p>
                <strong>Release date:</strong>
                <p>12/07/2017</p>

                <button type="button">Edit
                    <!-- <FiEdit size={20} color="#251fc5" /> -->
                </button>    
                <button type="button">Remove
                    <!-- <FiTrash2 size={20} color="#251fc5" /> -->
                </button>    
            </li> 
            <li>
                <strong>Title:</strong>
                <p>Docker Deep Dive</p>
                <strong>Author:</strong>
                <p>Nigel Poulton</p>
                <strong>Price:</strong>
                <p>R$ 47,90</p>
                <strong>Release date:</strong>
                <p>12/07/2017</p>

                <button type="button">Edit
                    <!-- <FiEdit size={20} color="#251fc5" /> -->
                </button>    
                <button type="button">Remove
                    <!-- <FiTrash2 size={20} color="#251fc5" /> -->
                </button>
            </li> 
            <li>
                <strong>Title:</strong>
                <p>Docker Deep Dive</p>
                <strong>Author:</strong>
                <p>Nigel Poulton</p>
                <strong>Price:</strong>
                <p>R$ 47,90</p>
                <strong>Release date:</strong>
                <p>12/07/2017</p>

                <button type="button">Edit
                    <!-- <FiEdit size={20} color="#251fc5" /> -->
                </button>    
                <button type="button">Remove
                    <!-- <FiTrash2 size={20} color="#251fc5" /> -->
                </button>    
            </li> 
            <li>
                <strong>Title:</strong>
                <p>Docker Deep Dive</p>
                <strong>Author:</strong>
                <p>Nigel Poulton</p>
                <strong>Price:</strong>
                <p>R$ 47,90</p>
                <strong>Release date:</strong>
                <p>12/07/2017</p>

                <button type="button">Edit
                    <!-- <FiEdit size={20} color="#251fc5" /> -->
                </button>    
                <button type="button">Remove
                    <!-- <FiTrash2 size={20} color="#251fc5" /> -->
                </button>    
            </li> 
        </ul>
    </div>
</template>

<script>
export default {
    props: ['username'],
    name: 'BookList',
    data() {
        return {
            booklist: []
        }
    },
    methods: {
        getBooks() {
            
        }
    }
}
</script>

<style scoped>
.book-container {
    width: 100%;
    max-width: 1180px;
    padding: 0 30px;
    margin: 32px auto;
}

.book-container header {
    display: flex;
    align-items: center;
}

.book-container header span {
    font-size: 20px;
    margin-left: 24px;
}

.book-container header span strong {
    color: #251fc5;
}

.book-container header img {
    height: 64px;
}

.book-container header a {
    width: 260px;
    margin-left: auto;
    margin-top: 0;
}

.book-container header button {
    height: 60px;
    width: 60px;
    border-radius: 4px;
    border: 1px solid #dcdce6;
    background: transparent;
    margin-left: 16px;
    transition: border-color 0.5s;
}

.book-container header button:hover {
    border-color: #251fc5;
}

.book-container h1 {
    margin-top: 80px;
    margin-bottom: 24px;
}

.book-container ul {
    display: grid;
    grid-template-columns: repeat(2, 1fr);
    grid-gap: 24px;
    list-style: none;
}

.book-container ul li {
    background: white;
    padding: 24px;
    border-radius: 8px;
    position: relative;
}

.book-container ul li button {
    position: absolute;
    right: 24px;
    top: 24px;
    border: 0;
    background: transparent;
}

.book-container ul li button + button {
    top: 50px;
}

.book-container ul li button:hover {
    opacity: 0.8;
}

.book-container ul li strong {
    display: block;
    margin-bottom: 16px;
    color: #41414d;
}

.book-container ul li p + strong {
    margin-top: 32px;
}

.book-container ul li p {
    color: #737380;
    line-height: 21px;
    font-size: 16px;
}
</style>