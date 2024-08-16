class DiffleHelman:
    # Constantes G e N usadas no algoritmo Diffie-Hellman
    G = 7
    N = 23

    @staticmethod
    def calculate_r(private_number):
        """
        Calcula o valor de r usando a fórmula (G^private_number) % N
        onde G é a base, private_number é o número privado e N é o módulo.
        
        :param private_number: Número privado usado no cálculo
        :return: Valor calculado de r
        """
        return pow(DiffleHelman.G, private_number, DiffleHelman.N)

    @staticmethod
    def calculate_k(private_number, R):
        """
        Calcula a chave compartilhada k usando a fórmula (R^private_number) % N
        onde R é o valor recebido do outro participante, private_number é o número privado e N é o módulo.
        
        :param private_number: Número privado usado no cálculo
        :param R: Valor recebido do outro participante
        :return: Chave compartilhada calculada
        """
        return pow(R, private_number, DiffleHelman.N)