CREATE TABLE IF NOT EXISTS public.companies (
    id SERIAL PRIMARY KEY,
    name VARCHAR(100) NOT NULL,
    email VARCHAR(100) NOT NULL,
    apikey VARCHAR(100) NOT NULL,
    apisecret VARCHAR(100) NOT NULL,
    createdat TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP NOT NULL
);

CREATE TABLE IF NOT EXISTS public.orders (
    id SERIAL PRIMARY KEY,
    companyid INTEGER NOT NULL,
    amount NUMERIC(12, 2) NOT NULL,
    currency VARCHAR(3) NOT NULL,
    status VARCHAR(20) NOT NULL,
    createdat TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP NOT NULL,
    FOREIGN KEY (companyid) REFERENCES public.companies (id)
);

CREATE TABLE IF NOT EXISTS public.transactions (
    id SERIAL PRIMARY KEY,
    orderid INTEGER NOT NULL,
    cardnumber VARCHAR(20),
    cardexpirydate DATE,
    createdat TIMESTAMPTZ DEFAULT CURRENT_TIMESTAMP NOT NULL,
    FOREIGN KEY (orderid) REFERENCES public.orders (id)
);