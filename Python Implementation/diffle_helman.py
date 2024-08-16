class DiffleHelman:
    G = 7
    N = 23

    @staticmethod
    def calculate_r(private_number):
        return pow(DiffleHelman.G, private_number, DiffleHelman.N)

    @staticmethod
    def calculate_k(private_number, R):
        return pow(R, private_number, DiffleHelman.N)