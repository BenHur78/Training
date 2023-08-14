package validation;

import java.util.HashMap;

/**
 * Hello world!
 */
public final class App {
    private App() {
    }

    /**
     * Says hello to the world.
     * 
     * @param args The arguments of the program.
     */
    public static void main(String[] args) {
        System.out.println("Hello World!");
    }

    public static HashMap<String, Object> validateFocusEvent(
            HashMap<String, Object> tokens,
            HashMap<String, HashMap<String, HashMap<String, String>>> refdata) throws IllegalArgumentException {

        System.out.println("validateFocusEvent entered.");

        // Validar se campos existem
        if (!tokens.containsKey("msisdn")) {
            throw new IllegalArgumentException("O campo msisdn não existe no registo.");
        }

        if (!tokens.containsKey("balance_before_topup")) {
            throw new IllegalArgumentException("O campo balance_before_topup não existe no registo.");
        }

        if (!tokens.containsKey("topup_propensity_2")) {
            throw new IllegalArgumentException("O campo topup_propensity_2 não existe no registo.");
        }

        if (!tokens.containsKey("topup_propensity_3")) {
            throw new IllegalArgumentException("O campo topup_propensity_3 não existe no registo.");
        }

        if (!tokens.containsKey("topup_propensity_4")) {
            throw new IllegalArgumentException("O campo topup_propensity_4 não existe no registo.");
        }

        System.out.println("validateFocusEvent - Validar se campos existem -  Ok.");

        // Validar tipo de dados
        if (tokens.get("balance_before_topup") instanceof Double) {
            Double balanceBeforeTopup = (Double) tokens.get("balance_before_topup");
            String valueStr = balanceBeforeTopup.toString();
            int decimalSeparatorIndex = valueStr.indexOf('.') != -1 ? valueStr.indexOf('.') : valueStr.indexOf(',');
            int decimalPlaces = 0;
            if (decimalSeparatorIndex != -1) {
                decimalPlaces = valueStr.length() - decimalSeparatorIndex - 1;
            }

            if (decimalPlaces > 2) {
                System.out.println("validateFocusEvent - decimalPlaces = " + decimalPlaces);
                throw new IllegalArgumentException(
                        "O campo balance_before_topup não é um double com duas casas decimais.");
            }

        } else {
            throw new IllegalArgumentException("O campo balance_before_topup não é um double.");
        }

        if (!(tokens.get("topup_propensity_2") instanceof Long)) {
            throw new IllegalArgumentException("O campo topup_propensity_2 não é um inteiro.");
        }

        if (!(tokens.get("topup_propensity_3") instanceof Long)) {
            throw new IllegalArgumentException("O campo topup_propensity_3 não é um inteiro.");
        }

        if (!(tokens.get("topup_propensity_4") instanceof Long)) {
            throw new IllegalArgumentException("O campo topup_propensity_4 não é um inteiro.");
        }

        System.out.println("validateFocusEvent - Validar tipo de dados  -  Ok.");

        // Validar os limites dos valores dos campos
        Double balanceBeforeTopup = (Double) tokens.get("balance_before_topup");
        if (!(balanceBeforeTopup >= -1000 && balanceBeforeTopup <= 1000)) {
            throw new IllegalArgumentException("O campo balance_before_topup com o valor '" + balanceBeforeTopup
                    + "' não tem um valor superior ou igual a -1000 ou inferior ou igual a 1000.");
        }

        Long topupPropensity2 = (Long) tokens.get("topup_propensity_2");
        if (!(topupPropensity2 >= 0 && topupPropensity2 <= 100)) {
            throw new IllegalArgumentException("O campo topup_propensity_2 com o valor '" + topupPropensity2
                    + "' não tem um valor superior ou igual a 0 ou inferior ou igual a 100.");
        }

        Long topupPropensity3 = (Long) tokens.get("topup_propensity_3");
        if (!(topupPropensity3 >= 0 && topupPropensity3 <= 100)) {
            throw new IllegalArgumentException("O campo topup_propensity_3 com o valor '" + topupPropensity3
                    + "' não tem um valor superior ou igual a 0 ou inferior ou igual a 100.");
        }

        Long topupPropensity4 = (Long) tokens.get("topup_propensity_4");
        if (!(topupPropensity4 >= 0 && topupPropensity4 <= 100)) {
            throw new IllegalArgumentException("O campo topup_propensity_4 com o valor '" + topupPropensity4
                    + "' não tem um valor superior ou igual a 0 ou inferior ou igual a 100.");
        }

        System.out.println("validateFocusEvent ended.");

        return tokens;
    }
}
